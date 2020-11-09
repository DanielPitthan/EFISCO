
/*
 * JS Helper to execute java scripts via Interops
 */
(function () {

    modal = (modalId) => {
        $(modalId).modal('show');
    };

    carousel = (options) => {
        $('.carousel').carousel(options);
    };

    toast = (options) => {
        $(options.entity).toast('show');
    };

    enableMultiFile = () => {
        $("#uploadImg input").attr("multiple", "true");
    };

   
})();
