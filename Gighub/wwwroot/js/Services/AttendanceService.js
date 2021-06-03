
var AttendanceService = function () {

    var attendance = function (gigId, done, fail) {
        $.post('/api/Attendances/attend/' + gigId)
            .done((response) => { done(response) })
            .fail(fail);
    }
    return { attendance };
}();








