function showStarRatingForRoutesList() {
    let ratingElements = document.getElementsByClassName("route-star-rating");
    for (let i = 0; i < ratingElements.length; i++) {
        let markupElementId = ratingElements[i].id;
        let valueElementId = ratingElements[i].firstElementChild.id;
        $("#" + markupElementId).starRating({
            starSize: 25,
            initialRating: $("#" + valueElementId).val(),
            readOnly: true,
        });
    }
}
function showStarRatingForSingleRoute() {
    $(".route-rating").starRating({
        starSize: 25,
        initialRating: $("#rating-value").val(),
        readOnly: true,
    });
}
function createNewStarRating() {
    $(".route-rating").starRating({
        starSize: 25,
        initialRating: 0,
        readOnly: false,
        useFullStars: true,
        disableAfterRate: false,
        callback: function (currentRating, $el) {
            $("#rating-value").val(currentRating);
        }
    });
}
function createEditStarRating(initialRatingValue) {
    $(".route-rating").starRating({
        starSize: 25,
        initialRating: initialRatingValue,
        readOnly: false,
        useFullStars: true,
        disableAfterRate: false,
        callback: function (currentRating, $el) {
            $("#rating-value").val(currentRating);
        }
    });
}