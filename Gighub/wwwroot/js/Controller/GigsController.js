var GigsController = function (attendanceService ) {

    var button;

    var init = function (container) {
        $(container).on("click", ".toggle-attendance", toggleAttendance)
        
    }

    var toggleAttendance = function (e) {
        button = $(e.target);
        var gigId = button.attr('data-id');
        attendanceService.attendance(gigId, done, fail);
    }

    var done = function (response) {
        if (response.status == 1) {
            button.removeClass('btn-light').addClass('btn-info').text('Going');
            toastr.success(response.message);
        }
        else {
            button.removeClass('btn-info').addClass('btn-light').text('Going?');
            toastr.info(response.message);
        }
    }

    var fail = function () {
        toastr.error(`Something went wrong.`);
    }

    return { init };
}(AttendanceService);