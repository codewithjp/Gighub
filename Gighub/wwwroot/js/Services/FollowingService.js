
var FollowingService = function () {
	

	var following = function (artistId,done,fail) {
		$.ajax({

			url: '/api/Followings/follow',
			data: JSON.stringify({ followeeId: artistId }),
			contentType: 'application/json',
			method: 'Post'
		})
			.done((response) => { done(response) })
			.fail(fail)

	}

	return { following };

}();