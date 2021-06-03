
var DetailsController = function (followingService) {
	var button;

	var init = function (container) {
		
		$(container).on("click", ".toggle-follow", toggleFollowing)

	};

	var toggleFollowing = function (e) {
		button = $(e.target);
		var artistId = button.attr("data-id")
		followingService.following(artistId, done, fail)
	}

	var done = function (response) {
		if (response.status == 1) {
			button.removeClass('btn-outline-secondary').addClass('btn-info').text('Following');
			toastr.success(response.message);
		}
		else {
			button.removeClass('btn-info').addClass('btn-outline-secondary').text('Follow?');
			toastr.warning(response.message);
		}
	}

	var fail = function () {
		toastr.error(`Something went wrong.`);
    }

	return { init };
}(FollowingService);