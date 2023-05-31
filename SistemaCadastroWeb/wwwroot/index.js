sap.ui.define([
    "sap/ui/core/ComponentContainer"
], function(ComponentContainer) {
    'use strict';
    
    new ComponentContainer({
        name: "sap.ui.demo.cadastro",
        settings: {
            id: "cadastro"
        },
        async: true
    }).placeAt("conteudo")
});