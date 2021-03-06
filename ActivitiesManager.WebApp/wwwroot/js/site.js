﻿// Write your JavaScript code.

var _submitForm = false;
const LoadComponentAjax = '<center><div class="load-component-ajax"><div class="loader"></div></div></center>';
function CustomErrorComponentAjax(elementId) {
    var ErrorComponentAjax =
        '<div id="sidebar-noti-error" class="p-m border-bottom">' +
        '<center>' +
        '<i class="pe-7s-attention" style="font-size: 100px"></i>' +
        '<p>Ocurrió un error.</p>' +
        '<p><button type="button" data-reload-elementid="#' + elementId + '" id="reload-' + elementId + '" class="btn btn-default btnReloadComponentAjax">Recargar</button><p>'
        '</center>' +
        '</div>';
    return ErrorComponentAjax;
}

$(function () {
    $("div.innerLoadComponentAjax").html(LoadComponentAjax);
    $("div.load-component-ajax").hide();
    $("form.form-validate").attr("onsubmit", "javascript: return submitForm(this);")

    $("body").on("click", "a.buttonLoadComponentAjax", function (event) {
        event.preventDefault();

        var thisId = $(this).attr("id");
        var targetId = $(this).attr("href");
        var $target = $("div" + targetId);
        var viewUrlAjax = $($target).attr("data-view-url-ajax");
        var viewParametersAjax = $($target).attr("data-view-parameters-ajax");

        var object = JSON.parse(viewParametersAjax);
        var formData = getFormData(object);

        showLoadComponent(targetId);

        $.ajax({
            url: viewUrlAjax,
            type: "POST",
            cache: true,
            async: true,
            contentType: false,
            processData: false,
            data: formData,
            success: function (html) {
                $($target).html(html);
                hiddeLoadComponent(targetId);
            },
            error: function () {
                $($target).html(CustomErrorComponentAjax(thisId));
                hiddeLoadComponent(targetId);
            }
        });
    });

    $("body").on("click", "button.btnReloadComponentAjax", function (event) {
        event.preventDefault();

        var targetElementId = $(this).attr("data-reload-elementid");
        if (targetElementId) {
            $(targetElementId).click();
        }
    });

    $("body").on("click", "input.btn-submit", function (event) {
        event.preventDefault();
        var targetFormId = $(this).parents("form").attr("id");
        $("form#" + targetFormId).submit();
    });

    function getFormData(object) {
        var form_data = new FormData();
        var props = Object.getOwnPropertyNames(object);
        for (var i = 0; i < props.length; i++) {
            let prop = props[i];
            form_data.append(prop, object[prop]);
        }
        return form_data;
    }

    function hiddeLoadComponent(elementId) {
        $(elementId + " div.load-component-ajax").hide();
    }

    function showLoadComponent(elementId) {
        $(elementId + " div.load-component-ajax").show();
    }

    function handlerError(message, view, moreInfo) {
        console.error(message, view, moreInfo);
    }

    function isValid(formId) {
        if (formId) {
            return true;
        } else {
            return false;
        }
    }
});


function submitForm(target) {
    var $this = $(target);
    if (true) {
        let message = $this.data('confirm-message') || '<h3>¿Desea continuar?</h3>Revise la información antes de continuar.';
        if (confirm(message)) {
            _submitForm = true;
            return;
            $this.submit();
        } else {
            _submitForm = false;
        }
    } else {
        _submitForm = false;
    }
    return _submitForm;
}