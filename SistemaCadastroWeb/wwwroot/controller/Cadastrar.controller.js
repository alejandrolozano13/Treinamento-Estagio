sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "sap/ui/core/routing/History",
    "sap/m/MessageBox"
], function (Controller, JSONModel, History, MessageBox) {
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
            })
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

        aoSalvarCliente: function () {
            debugger
            let oFileUploader = this.byId("fileUploader");
            let cliente = this.getView().getModel("cliente").getData();

            this.getBase64(oFileUploader.oFileUpload.files[0])
                .then(base64 => {
                    let string64 = base64.split(",")[1];
                    cliente.imagemUsuario = string64;
                    fetch("api/Cliente", {
                        method: 'POST',
                        headers: {

                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(cliente)
                    })
                        .then(resp => resp.json())
                        .then(response => {
                            if (response.status == 400) {
                                MessageBox.error("Erro ao cadastrar cliente");
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
                        .catch(function (error) {
                            console.error(error);
                        });
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