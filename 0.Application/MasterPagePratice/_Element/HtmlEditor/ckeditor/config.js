/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {

    config.resize_enabled = true;
    config.scayt_autoStartup = false;
    config.allowedContent = true;
    config.enterMode = CKEDITOR.ENTER_BR;
    config.forcePasteAsPlainText = false;
    config.basicEntities = true;
    config.entities = true;
    config.entities_latin = false;
    config.entities_greek = false;
    config.entities_processNumerical = false;
    config.fillEmptyBlocks = function (element) { return true; };

    //ckfinder
    config.filebrowserBrowseUrl = "/_Element/HtmlEditor/ckfinder/ckfinder.html";
    config.filebrowserImageBrowseUrl = "/_Element/HtmlEditor/ckfinder/ckfinder.html?type=Images";
    config.filebrowserFlashBrowseUrl = "/_Element/HtmlEditor/ckfinder/ckfinder.html?type=Flash";
    config.filebrowserUploadUrl = "/_Element/HtmlEditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files";
    config.filebrowserImageUploadUrl = "/_Element/HtmlEditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images";
    config.filebrowserFlashUploadUrl = "/_Element/HtmlEditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash";

    config.extraPlugins = 'youtube';

    config.toolbar = 'Templete1';
    config.toolbar_Templete1 = [
		['Source', '-', 'NewPage', 'Save', 'Preview', '-', 'Templates'],
        ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Print', 'SpellChecker', 'Scayt'],
        ['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll'],
         '/',
        ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote'],
        ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
        ['Link', 'Unlink', 'Anchor'],
        ['Image', 'Youtube', 'Table', 'CreateDiv', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak'],
        '/',
        ['Font', 'FontSize'],
        ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
        ['TextColor', 'BGColor'],
        ['RemoveFormat'],
        ['Maximize', 'ShowBlock']
    ];

    config.removeDialogTabs = 'image:Link;link:link;image:advanced;link:advanced;';
};
