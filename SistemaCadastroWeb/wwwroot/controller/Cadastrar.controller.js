sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "sap/ui/core/routing/History",
    "sap/m/MessageBox",
    "../validacoes/Validacoes",
    "sap/ui/core/BusyIndicator"
], function (Controller, JSONModel, History, MessageBox, Validacoes, BusyIndicator) {
    'use strict';

    return Controller.extend("sap.ui.demo.cadastro.controller.Cadastrar", {
        onInit: function () {
            let oRouter = this.getOwnerComponent().getRouter();
            oRouter.getRoute("cadastrar").attachPatternMatched(this._aoCoincidirRota, this);
            oRouter.getRoute("editarCliente").attachPatternMatched(this._aoCoincidirRotaDeEdicao, this);
        },

        handleUploadComplete: function (oEvent) {
            let sResponse = oEvent.getParameter("response"),
                iHttpStatusCode = parseInt(/\d{3}/.exec(sResponse)[0]),
                sMessage;

            if (sResponse) {
                sMessage = iHttpStatusCode === 200 ? sResponse + " (Upload Success)" : sResponse + " (Upload Error)";
                MessageToast.show(sMessage);
            }
        },

        validarData: function (data, campoData) {
            let cliente = this.getView().getModel("cliente").getData();
            data = cliente.data;
            data = new Date(data).getFullYear();
            return Validacoes.validarData(data, campoData);
        },

        _aoCoincidirRota: function () {
            let modeloCliente = {
                nome: "",
                email: "",
                cpf: "",
                data: "",
                telefone: "",
                imagemUsuario: ""
            }
            this.getView().setModel(new JSONModel(modeloCliente), "cliente");
        },

        _aoCoincidirRotaDeEdicao(oEvent) {
            let id = oEvent.getParameter("arguments").id;
            this.carregarCliente(id);
        },

        getBase64(file) {
            return new Promise((resolve, reject) => {
                let reader = new FileReader();
                reader.readAsDataURL(file);
                reader.onload = () => {
                    resolve(reader.result);
                };
                reader.onerror = (error) => {
                    reject(error);
                };
            });
        },

        aoCancelarCadastro: function () {
            this.voltarAoMenu();
            this.aoLimparCampos();
            this.aoDevolverCamposVazios();
        },

        voltarAoMenu: function () {
            this.aoLimparCampos()
            var oRouter = this.getOwnerComponent().getRouter();
            oRouter.navTo("listaclientes", {}, true);
            this.aoDevolverCamposVazios();
        },

        validarNomeInput: function () {
            let nome = this.byId("campoNome");
            Validacoes.validarNome(nome.getValue(), nome);
            return nome.getValueState() === "None";
        },

        validarCpf: function () {
            let cpf = this.byId("campoCpf");
            Validacoes.validarCpf(cpf.getValue(), cpf);
            return cpf.getValueState() === "None";
        },

        validarTelefone: function () {
            let telefone = this.byId("campoTelefone");
            Validacoes.validarTelefone(telefone.getValue(), telefone);
            return telefone.getValueState() === "None";
        },

        validarEmail: function () {
            let email = this.byId("campoEmail");
            Validacoes.validarEmail(email.getValue(), email);
            return email.getValueState() === "None";
        },

        vaildarCampos: function () {
            let data = this.byId("campoData");
            let lista = []
            lista.push(this.validarNomeInput());
            lista.push(this.validarCpf());
            lista.push(this.validarEmail());
            lista.push(this.validarTelefone());
            lista.push(this.validarData(data.getValue(), data));

            return !lista.includes(false);
        },

        aoDevolverCamposVazios: function () {
            this.byId("campoNome").setValueState("None");
            this.byId("campoCpf").setValueState("None");
            this.byId("campoEmail").setValueState("None");
            this.byId("campoTelefone").setValueState("None");
            this.byId("campoData").setValueState("None");
        },

        carregarCliente: function (id) {
            let tela = this.getView();

            fetch(`api/Cliente/${id}`)
                .then(function (response) {
                    return response.json();
                })
                .then((data) => {
                    let arquivo = this.dataURLtoFile(data.imagemUsuario, "imagem.jpeg");
                    data.imagemUsuarioTraduzido = this.dataCreateObject(arquivo)
                    data.data = new Date(data.data);
                    tela.setModel(new JSONModel(data), "cliente")
                })
                .catch(function (error) {
                    console.error(error);
                });
        },

        dataCreateObject(file) {
            return URL.createObjectURL(file);
        },

        dataURLtoFile(bse64, filename) {
            let bstr = atob(bse64)
            let n = bstr.length
            let u8arr = new Uint8Array(n)
            while (n--) {
                u8arr[n] = bstr.charCodeAt(n);
            }
            return new File([u8arr], filename, { type: "image/jpeg" });
        },

        aoCarregarImagem: async function () {
            let oFileUploader = this.byId("fileUploader");
            let arquivo = oFileUploader.oFileUpload.files[0];
            if (!!arquivo) {
                let base64 = await this.getBase64(arquivo);
                let string64 = base64.split(",")[1];
                return string64;
            };
        },

        aoSalvarCliente: async function () {
            let cliente = this.getView().getModel("cliente").getData();
            if (this.vaildarCampos() && cliente.id) {
                cliente.imagemUsuario = await this.aoCarregarImagem();
                this.fetchEditar();
            }
            else {
                if (this.vaildarCampos()) {
                    cliente.imagemUsuario = await this.aoCarregarImagem();
                    this.fetchSalvar()
                } else {
                    MessageBox.error("Verifique as informações por gentileza, informações inválidas.");
                }
            }
        },

        aoLimparImagem: function(){
            let cliente = this.getView().getModel("cliente").getData();
            cliente.imagemUsuario = null;
            this.byId("fileUploader").setValue("");
        },

        fetchSalvar() {
            let cliente = this.getView().getModel("cliente").getData();
            fetch("api/Cliente", {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(cliente)
            }).then(resp => {
                if (resp.status != 200) {
                    return resp.text();
                }
                return resp.json();
            }).then(response => {
                if (typeof response == "string") {
                    MessageBox.error(response);
                } else {
                    MessageBox.information("Ciente atualizado com sucesso", {
                        EmphasizedAction: MessageBox.Action.OK,
                        actions: [MessageBox.Action.OK], onClose: (acao) => {
                            if (acao == MessageBox.Action.OK) {
                                this.aoNavegar(response);
                            }
                        }
                    })
                }
            }).catch((error) => {
                MessageBox.error(error);
            });
        },

        fetchEditar() {
            // BusyIndicator.show()
            let cliente = this.getView().getModel("cliente").getData();
            fetch(`api/Cliente/${cliente.id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(cliente)
            }).then(resp => {
                if (resp.status != 200) {
                    return resp.text();
                }
                // BusyIndicator.hide();
                return resp.json();
            }).then(response => {
                if (typeof response == "string") {
                    MessageBox.error(response);
                } else {
                    MessageBox.information("Ciente atualizado com sucesso", {
                        EmphasizedAction: MessageBox.Action.OK,
                        actions: [MessageBox.Action.OK], onClose: (acao) => {
                            if (acao == MessageBox.Action.OK) {
                                this.aoNavegar(cliente.id);
                            }
                        }
                    })
                }
            }).catch((error) => {
                MessageBox.error(error);
            });
        },

        aoNavegar: function (id) {
            BusyIndicator.show(0)
            
            let oRouter = this.getOwnerComponent().getRouter();
            oRouter.navTo("detail", {
                id: id
            });
            BusyIndicator.hide();
        },

        aoLimparCampos() {
            let nome = this.byId("campoNome");
            let cpf = this.byId("campoCpf");
            let email = this.byId("campoEmail");
            let telefone = this.byId("campoTelefone");
            let data = this.byId("campoData");

            nome.setValue("");
            cpf.setValue("");
            email.setValue("");
            telefone.setValue("");
            data.setValue("");
        },
    })
});
