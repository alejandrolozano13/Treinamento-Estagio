sap.ui.define([
    "sap/ui/core/UIComponent",
    "sap/ui/model/json/JSONModel",
    "sap/ui/model/resource/ResourceModel"
 ], function (UIComponent, JSONModel, ResourceModel) {
    "use strict";
    return UIComponent.extend("sap.ui.demo.cadastro.Component", {
        metadata : {
            interfaces: ["sap.ui.core.IAsyncContentCreation"],
            manifest: "json"
      },
       init : function () {
        UIComponent.prototype.init.apply(this, arguments);
        //   set i18n model
          var i18nModel = new ResourceModel({
             bundleName: "sap.ui.demo.cadastro.i18n.i18n"
          });
          this.setModel(i18nModel, "i18n");

          this.getRouter().initialize();
       }
    });
 });
 