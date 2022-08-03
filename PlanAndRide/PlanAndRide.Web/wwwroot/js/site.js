// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function AutocomplePlaces () {
    const originInput = new google.maps.places.Autocomplete(document.getElementById('StartingLocation'));
    google.maps.event.addListener(originInput, 'place_changed', function () {
        const placeDetails = originInput.getPlace();
        document.getElementById('StartingLocation').value = placeDetails.formatted_address;
        document.getElementById('StartingLatitude').value = placeDetails.geometry.location.lat();
        document.getElementById('StartingLongitude').value = placeDetails.geometry.location.lng();
    });

    const destinationInput = new google.maps.places.Autocomplete(document.getElementById('DestinationLocation'));
    google.maps.event.addListener(destinationInput, 'place_changed', function () {
        const placeDetails = destinationInput.getPlace();
        document.getElementById('DestinationLocation').value = placeDetails.formatted_address;
        document.getElementById('DestinationLatitude').value = placeDetails.geometry.location.lat();
        document.getElementById('DestinationLongitude').value = placeDetails.geometry.location.lng();
    });
}

