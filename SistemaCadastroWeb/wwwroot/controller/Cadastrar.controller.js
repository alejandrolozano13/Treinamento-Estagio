sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "sap/ui/core/routing/History",
    "sap/m/MessageBox",
    "../validacoes/Validacoes"
], function (Controller, JSONModel, History, MessageBox, Validacoes) {
    'use strict';

    return Controller.extend("sap.ui.demo.cadastro.controller.Cadastrar", {
        onInit: function () {
            let oRouter = this.getOwnerComponent().getRouter();
            oRouter.getRoute("cadastrar").attachPatternMatched(this._aoCoincidirRota, this);
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
            this.voltarAoMenu()
            this.aoLimparCampos()
        },

        voltarAoMenu: function () {
            this.aoLimparCampos()
            var oRouter = this.getOwnerComponent().getRouter();
            oRouter.navTo("listaclientes", {}, true);
        },

        validarNomeInput : function(){
            let nome = this.byId("campoNome");
            Validacoes.validarNome(nome.getValue(), nome);
            return nome.getValueState() == "None";
        },

        testeNome : async function(){
            return Validacoes.validarNome();
        },

        vaildarCampos: function(){
            let nome = this.byId("campoNome").getValue();
            let cpf = this.byId("campoCpf").getValue();
            let tel = this.byId("campoTelefone").getValue();
            let email = this.byId("campoEmail").getValue();

            this.validarNomeInput();
            Validacoes.validarNome(nome, this.byId("campoNome"));
            Validacoes.validarCpf(cpf, this.byId("campoCpf"));
            Validacoes.validarTelefone(tel, this.byId("campoTelefone"));
            Validacoes.validarEmail(email, this.byId("campoEmail"));

            return this.validarNomeInput() && this.validarNomeInput()
        },

        aoSalvarCliente: async function () {
            let nome = this.byId("campoNome").getValue();
            let cpf = this.byId("campoCpf").getValue();
            let tel = this.byId("campoTelefone").getValue();
            let email = this.byId("campoEmail").getValue();

            // this.validarNomeInput();
            Validacoes.validarNome(nome, this.byId("campoNome"));
            Validacoes.validarCpf(cpf, this.byId("campoCpf"));
            Validacoes.validarTelefone(tel, this.byId("campoTelefone"));
            Validacoes.validarEmail(email, this.byId("campoEmail"));

            let cliente = this.getView().getModel("cliente").getData();

            let oFileUploader = this.byId("fileUploader");
            let arquivo = oFileUploader.oFileUpload.files[0];
            if(!!arquivo){
                let base64 = await this.getBase64(arquivo);
                let string64 = base64.split(",")[1];
                cliente.imagemUsuario = string64;
            };

            if (cliente.data == "" || cliente.data == null) {

                delete cliente.data
            };

            fetch("api/Cliente", {
                method: 'POST',
                headers: {

                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(cliente)
            })
                .then(resp => {
                    if(resp.status != 200){
                        return resp.text();
                    }
                    return resp.json();
                })
                .then(response => {
                    if (typeof response == "string") {
                        MessageBox.error(response);
                    }
                    else {
                        MessageBox.information(
                            "Cliente criado com sucesso", {
                            EmphasizedAction: MessageBox.Action.OK,
                            actions: [MessageBox.Action.OK], onClose: (acao) => {
                                if (acao == MessageBox.Action.OK) {
                                    this.aoNavegar(response);
                                }
                            }
                        }
                        )
                    }
                })
                .catch((error)=> {
                    MessageBox.error(error);
                });
        
        },

        aoNavegar: function (id) {
            let oRouter = this.getOwnerComponent().getRouter();
            oRouter.navTo("detail", {
                id: id
            });
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
