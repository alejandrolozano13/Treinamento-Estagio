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
            oRouter.getRoute("cadastrarCliente").attachPatternMatched(this._aoCoincidirRota, this);
            oRouter.getRoute("editarCliente").attachPatternMatched(this._aoCoincidirRotaDeEdicao, this);
        },
        
        modeloClientes: function(JSONModel){
            const nomeModelo = "cliente";
            
            if (!JSONModel){
                return this.getView().getModel(nomeModelo);
            } else{
                return this.getView().setModel(JSONModel, nomeModelo);   
            }
        },

        validarData: function (data, campoData) {
            let cliente = this.modeloClientes().getData();
            if (cliente.data == "" || cliente.data == null) {
                delete cliente.data;
            } else {
                data = cliente.data;
                data = new Date(data).getFullYear();
            }
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
            this.modeloClientes(new JSONModel(modeloCliente));
        },

        _aoCoincidirRotaDeEdicao(oEvent) {
            let id = oEvent.getParameter("arguments").id;
            this.carregarCliente(id);
        },
        
        pegandoBase64(file) {
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
            this.limpandoCampos();
            this.devolvendoCamposVazios();
        },

        voltarAoMenu: function () {
            this.limpandoCampos()
            var oRouter = this.getOwnerComponent().getRouter();
            oRouter.navTo("listaDeclientes", {}, true);
            this.devolvendoCamposVazios();
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
        
        devolvendoCamposVazios: function () {
            const campoNulo = "None";
            this.byId("campoNome").setValueState(campoNulo);
            this.byId("campoCpf").setValueState(campoNulo);
            this.byId("campoEmail").setValueState(campoNulo);
            this.byId("campoTelefone").setValueState(campoNulo);
            this.byId("campoData").setValueState(campoNulo);
        },

        carregarCliente: function (id) {
            fetch(`api/Cliente/${id}`)
                .then(function (response) {
                    return response.json();
                })
                .then((data) => {
                    let arquivo = this.converteCaminhoParaImagem(data.imagemUsuario, "imagem.jpeg");
                    data.imagemUsuarioTraduzido = this.criandoArquivo(arquivo);
                    data.data = new Date(data.data);
                    this.modeloClientes(new JSONModel(data));
                })
                .catch(function (error) {
                    console.error(error);
                });
        },

        criandoArquivo(file) {
            return URL.createObjectURL(file);
        },

        converteCaminhoParaImagem(bse64, filename) {
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
                let base64 = await this.pegandoBase64(arquivo);
                let string64 = base64.split(",")[1];
                return string64;
            };
        },


        aoSalvarCliente: async function () {
            let cliente = this.modeloClientes().getData();
            if (cliente.id) {
                this.vaildarCampos()
                cliente.imagemUsuario = await this.aoCarregarImagem();
                this.fetchEditar();
            }
            else {
                this.vaildarCampos()
                cliente.imagemUsuario = await this.aoCarregarImagem();
                this.fetchSalvar()
            }
        },

        aoLimparImagem: function () {
            let cliente = this.modeloClientes().getData();
            cliente.imagemUsuario = null;
            this.byId("fileUploader").setValue("");
        },

        fetchSalvar() {
            const campoCpf = this.byId("campoCpf");
            let mensagemData = "";
            let cliente = this.modeloClientes().getData();
            if (cliente.data == null || cliente.data == "") { mensagemData = "Data Inválida" }
            fetch("api/Cliente", {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(cliente)
            })
                .then(resp => {
                    if (resp.status != 200) {
                        return resp.text();
                    }
                    return resp.json();
                }).then(response => {                    
                    if (typeof response == "string") {
                        MessageBox.error(response + "\n" + mensagemData);
                    } else {
                        MessageBox.information("Ciente adicionado com sucesso", {
                            EmphasizedAction: MessageBox.Action.OK,
                            actions: [MessageBox.Action.OK], onClose: (acao) => {
                                if (acao == MessageBox.Action.OK) {
                                    this.navegandoParaDetalhes(response);
                                }
                            }
                        })
                    }
                    if (typeof response == "string" && response.includes("CPF já existe!")) {
                        Validacoes.validandoCpfExistente(campoCpf);
                    }

                    if(typeof response == "string" && response.includes("CPF inválido")){
                        Validacoes.validarCpfValido(campoCpf);
                    }
                }).catch((error) => {
                    MessageBox.error(error);
                });
        },

        fetchEditar() {
            BusyIndicator.show()
            let cliente = this.modeloClientes().getData();
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
                BusyIndicator.hide();
                return resp.json();
            }).then(response => {
                if (typeof response == "string") {
                    MessageBox.error(response);
                } else {
                    MessageBox.information("Ciente atualizado com sucesso", {
                        EmphasizedAction: MessageBox.Action.OK,
                        actions: [MessageBox.Action.OK], onClose: (acao) => {
                            if (acao == MessageBox.Action.OK) {
                                this.navegandoParaDetalhes(cliente.id);
                            }
                        }
                    })
                }
                if(typeof response == "string" && response.includes("CPF inválido")){
                    this.byId("campoCpf").setValueState(sap.ui.core.ValueState.Error);
                }
            }).catch((error) => {
                MessageBox.error(error);
            });
        },

        navegandoParaDetalhes: function (id) {
            BusyIndicator.show()

            let oRouter = this.getOwnerComponent().getRouter();
            oRouter.navTo("detalhesDoCliente", {
                id: id
            });
            BusyIndicator.hide();
        },

        limpandoCampos() {
            const vazio = "";
            let nome = this.byId("campoNome");
            let cpf = this.byId("campoCpf");
            let email = this.byId("campoEmail");
            let telefone = this.byId("campoTelefone");
            let data = this.byId("campoData");

            nome.setValue(vazio);
            cpf.setValue(vazio);
            email.setValue(vazio);
            telefone.setValue(vazio);
            data.setValue(vazio);
        },
    })
});
