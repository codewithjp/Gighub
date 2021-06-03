
var NotificationController = function (notificationService) {

   
    var init = () => {
        notificationService.notification(displayCount, displayPopover);
    };


    var displayCount = (notifications) => {
        $('.js-notifications-count').text(notifications.length)
            .removeClass('hide').addClass('animate__animated animate__bounceInDown');
    }

    var displayPopover = (notifications) => {

        $('.notification').popover({
            title: 'Notifications',
            html: true,
            placement: 'bottom',
            content: content(notifications)

        }).on("shown.bs.popover", function () {
            notificationService.markAsRead(done, fail)
        })
    };


    var content = (notifications) => {
                var html = '';
                notifications.forEach(function (notification) {
                    if (notification.type === 1)
                        html = html + `<li><b>${notification.gigDto.artist.name} </b>
                                has canceled the gig at ${notification.gigDto.venue} at
                                ${moment(notification.gigDto.dateTime).format('DD MMM yyyy')}</li>`
                    if (notification.type === 2) {

                        html = html + `<li> <b>${notification.gigDto.artist.name} </b>
                                has changed the venue / datetime of the gig
                                from  ${notification.orignalVenue} / ${moment(notification.orignalDateTime).format('DD MMM yyyy HH:mm')}
                                to ${notification.gigDto.venue} / ${moment(notification.gigDto.dateTime).format('DD MMM yyyy HH:mm')}
                                </li >`

                    }
                });
                var list = `<ul class="notifications"> ${html} </ul>`;
                return list;
          
    };


    var done = () => {
        $('.js-notifications-count').text('').addClass('hide');
    };


    var fail = () => {
        console.log('There is some problem in MarkAsRead')
    };



    return { init };
}(NotificationService);