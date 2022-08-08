// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function PlacesAutocomplete() {
    
    const GetTownOrCity = function (addcomp) {
        const Intersect = function (a, b) {
            return new Set(a.filter(v => ~b.indexOf(v)));
        };
        if (typeof (addcomp) == 'object' && addcomp instanceof Array) {

            let order = ['locality', 'administrative_area_level_2', 'administrative_area_level_1'];

            for (let i = 0; i < addcomp.length; i++) {
                let obj = addcomp[i];
                let types = obj.types;
                if (Intersect(order, types).size > 0) return obj;
            }
        }
        return false;
    };

    $("document").ready(function () {
        const originInput = new google.maps.places.Autocomplete(document.getElementById('StartingLocation'));
        google.maps.event.addListener(originInput, 'place_changed', function () {
            const placeDetails = originInput.getPlace();
            document.getElementById('StartingLocation').value = placeDetails.formatted_address;
            document.getElementById('StartingLatitude').value = placeDetails.geometry.location.lat();
            document.getElementById('StartingLongitude').value = placeDetails.geometry.location.lng();
            let townOrCity = GetTownOrCity(placeDetails.address_components);
            if (townOrCity)
                document.getElementById("StartingCity").value = townOrCity.long_name;
        });

        const destinationInput = new google.maps.places.Autocomplete(document.getElementById('DestinationLocation'));
        google.maps.event.addListener(destinationInput, 'place_changed', function () {
            const placeDetails = destinationInput.getPlace();
            document.getElementById('DestinationLocation').value = placeDetails.formatted_address;
            document.getElementById('DestinationLatitude').value = placeDetails.geometry.location.lat();
            document.getElementById('DestinationLongitude').value = placeDetails.geometry.location.lng();
            let townOrCity = GetTownOrCity(placeDetails.address_components);
            if (townOrCity)
                document.getElementById("DestinationCity").value = townOrCity.long_name;
        });
    });
}
function LatLngToFormattedAddress() {
    $("document").ready(function () {
        const geocoder = new google.maps.Geocoder();
        const StartingLatLng = {
            lat: parseFloat(document.getElementById("StartingLatitude").value),
            lng: parseFloat(document.getElementById("StartingLongitude").value)
        }
        const DestinationLatLng = {
            lat: parseFloat(document.getElementById("DestinationLatitude").value),
            lng: parseFloat(document.getElementById("DestinationLongitude").value)
        }
        geocoder.geocode({ location: StartingLatLng }).then
            ((response) => {
                if (response.results[0]) {
                    document.getElementById("StartingLocation").value = response.results[0].formatted_address;
                }
            }).catch((e) => console.log("Geocoder failed due to: " + e));

        geocoder.geocode({ location: DestinationLatLng }).then
            ((response) => {
                if (response.results[0]) {
                    document.getElementById("DestinationLocation").value = response.results[0].formatted_address;
                }
            }).catch((e) => console.log("Geocoder failed due to: " + e));
    });
}

//function GetCityName() {
//    const Intersect = function (a, b) {
//        return new Set(a.filter(v => ~b.indexOf(v)));
//    };

//    const GetTownOrCity = function (addcomp) {
//        if (typeof (addcomp) == 'object' && addcomp instanceof Array) {

//            //let order=[ 'sublocality_level_1', 'neighborhood', 'locality', 'postal_town' ];
//            let order = ['locality', 'administrative_area_level_2', 'administrative_area_level_1'];

//            for (let i = 0; i < addcomp.length; i++) {
//                let obj = addcomp[i];
//                let types = obj.types;
//                if (Intersect(order, types).size > 0) return obj;
//            }
//        }
//        return false;
//    };

//    $("document").ready(function () {

//            const geocoder = new google.maps.Geocoder();

//            const StartingLatLng = {
//                lat: parseFloat(document.getElementById("StartingLatitude").value),
//                lng: parseFloat(document.getElementById("StartingLongitude").value)
//            }
//            const DestinationLatLng = {
//                lat: parseFloat(document.getElementById("DestinationLatitude").value),
//                lng: parseFloat(document.getElementById("DestinationLongitude").value)
//            }
//            geocoder.geocode({ location: StartingLatLng }).then
//                ((response) => {
//                    if (response.results[0]) {
//                        let addressComponents = response.results[0].address_components;
//                        let townOrCity = GetTownOrCity(addressComponents);
//                        if (townOrCity)
//                            document.getElementById("StartingCity").value = townOrCity.long_name;
//                    }
//                }).catch((e) => console.log("Geocoder failed due to: " + e));

//            geocoder.geocode({ location: DestinationLatLng }).then
//                ((response) => {
//                    if (response.results[0]) {
//                        let addressComponents = response.results[0].address_components;
//                        let townOrCity = GetTownOrCity(addressComponents);
//                        if (townOrCity)
//                            document.getElementById("DestinationCity").value = townOrCity.long_name;
//                    }
//                }).catch((e) => console.log("Geocoder failed due to: " + e));
//    });
//}








