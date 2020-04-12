/// <reference path="../lib/jquery/dist/jquery.js" />
/// <reference path="../lib/bootstrap-datepicker/dist/js/bootstrap-datepicker.js" />
/// <reference path="../lib/bootstrap/dist/js/bootstrap.js" />
window.blazorjs = {
    datepicker: function (ref, viewMode) {

        $('#datepicker').datepicker('destroy');

        $('#datepicker').datepicker({
            minViewMode: viewMode
            }).on('changeDate', function (e) {
                let date = e.date;

                 ref.invokeMethodAsync('NotifyChange', date.getFullYear(), date.getMonth(), date.getDate());
            });
    }
};