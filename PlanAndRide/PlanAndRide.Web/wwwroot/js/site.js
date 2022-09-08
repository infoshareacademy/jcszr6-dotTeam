// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var originLatLng, destinationLatLng;
var originMapMarker, destinationMapMarker;
var map;
var directionsService, directionsRenderer;


function createMapWithRouteDetails() {
    $("document").ready(
        () => {
            originLatLng = {
                lat: parseFloat(document.getElementById("StartingLatitude").value),
                lng: parseFloat(document.getElementById("StartingLongitude").value)
            }
            destinationLatLng = {
                lat: parseFloat(document.getElementById("DestinationLatitude").value),
                lng: parseFloat(document.getElementById("DestinationLongitude").value)
            }

            initMap(originLatLng,destinationLatLng);
            codeLatLngToFormattedAddress(originLatLng, "StartingLocation");
            codeLatLngToFormattedAddress(destinationLatLng, "DestinationLocation");
        }
    );
}

function createMapInEditRouteMode() {
    $("document").ready(
        () => {
            originLatLng = {
                lat: parseFloat(document.getElementById("StartingLatitude").value),
                lng: parseFloat(document.getElementById("StartingLongitude").value)
            }
            destinationLatLng = {
                lat: parseFloat(document.getElementById("DestinationLatitude").value),
                lng: parseFloat(document.getElementById("DestinationLongitude").value)
            }

            originElementsId = {
                addressElementId: "StartingLocation",
                latElementId: "StartingLatitude",
                lngElementId: "StartingLongitude",
                cityElementId: "StartingCity"
            }
            destinationElementsId = {
                addressElementId: "DestinationLocation",
                latElementId: "DestinationLatitude",
                lngElementId: "DestinationLongitude",
                cityElementId: "DestinationCity"
            }


            initMap(originLatLng, destinationLatLng);
            directionsRenderer.setOptions({draggable:true});
            codeLatLngToFormattedAddress(originLatLng, "StartingLocation");
            codeLatLngToFormattedAddress(destinationLatLng, "DestinationLocation");
            placesAutocomplete(originElementsId, destinationElementsId);
        }
    );
}


function createMapInCreateRouteMode() {
    $("document").ready(
        () => {
            originElementsId = {
                addressElementId: "StartingLocation",
                latElementId: "StartingLatitude",
                lngElementId: "StartingLongitude",
                cityElementId: "StartingCity"
            }
            destinationElementsId = {
                addressElementId: "DestinationLocation",
                latElementId: "DestinationLatitude",
                lngElementId: "DestinationLongitude",
                cityElementId: "DestinationCity"
            }


            initEmptyMap();
            directionsRenderer.setOptions({ draggable: true });
            placesAutocomplete(originElementsId, destinationElementsId);
        }
    );
}


function initEmptyMap() {
    let myCenter = { lat: 52.218467, lng: 19.134643 }; //Poland middle point
    let mapOptions = { center: myCenter, zoom: 5, scrollwheel: false, draggable: true, mapTypeId: google.maps.MapTypeId.ROADMAP };
    map = new google.maps.Map(document.getElementById("map"), mapOptions);
    directionsService = new google.maps.DirectionsService();
    directionsRenderer = new google.maps.DirectionsRenderer();
    directionsRenderer.setMap(map);
}

function initMap(originLatLng, destLatLng) {

    initEmptyMap();
    originMapMarker = new google.maps.Marker({ position: originLatLng });
    destinationMapMarker = new google.maps.Marker({ position: destLatLng });
    originMapMarker.setPosition(originLatLng);
    destinationMapMarker.setPosition(destLatLng);
    originMapMarker.setMap(map);
    destinationMapMarker.setMap(map);
    calcRouteAndFitMap(map, [originLatLng, destinationLatLng]);
}

function calcRouteAndFitMap(map, LatLngArr) {

    function calcRoute() {
        let start = originMapMarker.getPosition();
        let end = destinationMapMarker.getPosition();
        let request = {
            origin: start,
            destination: end,
            travelMode: 'DRIVING'
        };
        directionsService.route(request, function (result, status) {
            if (status == 'OK') {
                originMapMarker.setVisible(false);
                destinationMapMarker.setVisible(false);
                directionsRenderer.setDirections(result);
            }
        });
    }

    if (LatLngArr.length == 0)
        return;
    if (LatLngArr.length == 1) {
        map.setCenter(LatLngArr[0]);
        map.setZoom(10);
        return;
    }
    let bounds = new google.maps.LatLngBounds();
    LatLngArr.forEach(x => bounds.extend(x));
    map.fitBounds(bounds);
    calcRoute();

}

function placesAutocomplete(origin,destination) {

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

    const originAddressInput = new google.maps.places.Autocomplete(document.getElementById(origin.addressElementId));
    google.maps.event.addListener(originAddressInput, 'place_changed', function () {
        const placeDetails = originAddressInput.getPlace();
        document.getElementById(origin.addressElementId).value = placeDetails.formatted_address;
        document.getElementById(origin.latElementId).value = placeDetails.geometry.location.lat();
        document.getElementById(origin.lngElementId).value = placeDetails.geometry.location.lng();

        let townOrCity = GetTownOrCity(placeDetails.address_components);
        if (townOrCity)
            document.getElementById(origin.cityElementId).value = townOrCity.long_name;

        setOriginMarkerPosition();
    });
    const destinationAddressInput = new google.maps.places.Autocomplete(document.getElementById(destination.addressElementId));
    google.maps.event.addListener(destinationAddressInput, 'place_changed', function () {
        const placeDetails = destinationAddressInput.getPlace();
        document.getElementById(destination.addressElementId).value = placeDetails.formatted_address;
        document.getElementById(destination.latElementId).value = placeDetails.geometry.location.lat();
        document.getElementById(destination.lngElementId).value = placeDetails.geometry.location.lng();

        let townOrCity = GetTownOrCity(placeDetails.address_components);
        if (townOrCity)
            document.getElementById(destination.cityElementId).value = townOrCity.long_name;

        setDestinationMarkerPosition();
    });
}

function setOriginMarkerPosition() {
    let markers = [];
    let newOriginLatLng, currentDestinationLatLng;

    newOriginLatLng = {
        lat: parseFloat(document.getElementById('StartingLatitude').value),
        lng: parseFloat(document.getElementById('StartingLongitude').value)
    };

    if (originMapMarker == null) {
        originMapMarker = new google.maps.Marker(
            {
                position: newOriginLatLng,
                title: "START",
            });
        originMapMarker.setMap(map);
    }
    else {
        originMapMarker.setPosition(newOriginLatLng);
    }

    markers.push(newOriginLatLng);

    if (destinationMapMarker != null) {
        currentDestinationLatLng = {
            lat: parseFloat(document.getElementById('DestinationLatitude').value),
            lng: parseFloat(document.getElementById('DestinationLongitude').value)
        };
        markers.push(currentDestinationLatLng);
    }

    markers.sort((a, b) => a.lng - b.lng);
    calcRouteAndFitMap(map, markers);
}

function setDestinationMarkerPosition() {
    let markers = [];
    let currentOriginLatLng, newDestinationLatLng;

    newDestinationLatLng = {
        lat: parseFloat(document.getElementById('DestinationLatitude').value),
        lng: parseFloat(document.getElementById('DestinationLongitude').value)
    };

    if (destinationMapMarker == null) {
        destinationMapMarker = new google.maps.Marker(
            {
                position: newDestinationLatLng,
                title: "END",
            });
        destinationMapMarker.setMap(map);
    }
    else {
        destinationMapMarker.setPosition(newDestinationLatLng);
    }

    markers.push(newDestinationLatLng);

    if (originMapMarker != null) {
        currentOriginLatLng = {
            lat: parseFloat(document.getElementById('StartingLatitude').value),
            lng: parseFloat(document.getElementById('StartingLongitude').value)
        };
        markers.push(currentOriginLatLng);
    }

    markers.sort((a, b) => a.lng - b.lng);
    calcRouteAndFitMap(map, markers);
}

function codeLatLngToFormattedAddress(latLng, idAddressEl) {
        const geocoder = new google.maps.Geocoder();
        geocoder.geocode({ location: latLng }).then
            ((response) => {
                if (response.results[0]) {
                    document.getElementById(idAddressEl).value = response.results[0].formatted_address;
                }
            }).catch((e) => console.log("Geocoder failed due to: " + e));
}






(function ($) {
     //your standard jquery code goes here with $ prefix
    // best used inside a page with inline code, 
    // or outside the document ready, enter code here
})(jQuery);

var TxtType = function (el, toRotate, period) {
    this.toRotate = toRotate;
    this.el = el;
    this.loopNum = 0;
    this.period = parseInt(period, 10) || 2000;
    this.txt = '';
    this.tick();
    this.isDeleting = false;
};

TxtType.prototype.tick = function () {
    var i = this.loopNum % this.toRotate.length;
    var fullTxt = this.toRotate[i];

    if (this.isDeleting) {
        this.txt = fullTxt.substring(0, this.txt.length - 1);
    } else {
        this.txt = fullTxt.substring(0, this.txt.length + 1);
    }

    this.el.innerHTML = '<span class="wrap">' + this.txt + '</span>';

    var that = this;
    var delta = 200 - Math.random() * 100;

    if (this.isDeleting) { delta /= 2; }

    if (!this.isDeleting && this.txt === fullTxt) {
        delta = this.period;
        this.isDeleting = true;
    } else if (this.isDeleting && this.txt === '') {
        this.isDeleting = false;
        this.loopNum++;
        delta = 500;
    }

    setTimeout(function () {
        that.tick();
    }, delta);
};

window.onload = function () {
    var elements = document.getElementsByClassName('typewrite');
    for (var i = 0; i < elements.length; i++) {
        var toRotate = elements[i].getAttribute('data-type');
        var period = elements[i].getAttribute('data-period');
        if (toRotate) {
            new TxtType(elements[i], JSON.parse(toRotate), period);
        }
    }
    // INJECT CSS
    var css = document.createElement("style");
    css.type = "text/css";
    css.innerHTML = ".typewrite > .wrap { border-right: 0.08em solid #f44336}";
    document.body.appendChild(css);
};

var btn = $('#top-button');

$(window).scroll(function () {
    if ($(window).scrollTop() > 300) {
        btn.addClass('show');
    } else {
        btn.removeClass('show');
    }
});

btn.on('click', function (e) {
    e.preventDefault();
    $('html, body').animate({ scrollTop: 0 }, '300');
});
