

var NotificationService= function () {

    var notifications;
    var notification = (displayCount, displayPopover) => {
        $.getJSON("/api/Notifications/GetNotification", function (response) {
            notifications = response.dataenum;

        }).done(() => {
            if (notifications.length !== 0) {
                displayCount(notifications);
                displayPopover(notifications);
            }

        })
       
    };
   
    var markAsRead = (done, fail) => {
        $.post('/api/Notifications/MarkAsRead')
            .done(done)
            .fail(fail)
    };

    return { notification, markAsRead}
}();