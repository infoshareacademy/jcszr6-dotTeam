// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let originLatLng, destinationLatLng;
let originMapMarker, destinationMapMarker;
let map;


function mapWithRouteDetails() {
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

function mapWithEditRouteForm() {
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


            initMap(originLatLng,destinationLatLng);
            codeLatLngToFormattedAddress(originLatLng, "StartingLocation");
            codeLatLngToFormattedAddress(destinationLatLng, "DestinationLocation");
            placesAutocomplete(originElementsId, destinationElementsId);
        }
    );
}


function mapWithCreateRouteForm() {
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
            placesAutocomplete(originElementsId, destinationElementsId);
        }
    );
}


function initEmptyMap() {
    let myCenter = { lat: 52.218467, lng: 19.134643 }; 
    let mapOptions = { center: myCenter, zoom: 5, scrollwheel: false, draggable: true, mapTypeId: google.maps.MapTypeId.ROADMAP };
    map = new google.maps.Map(document.getElementById("map"), mapOptions);
}

function initMap(originLatLng, destLatLng) {

    initEmptyMap();

    let bounds;
    if (originLatLng.lng <= destLatLng.lng) {
        bounds = new google.maps.LatLngBounds(originLatLng, destLatLng);
    } else {
        bounds = new google.maps.LatLngBounds(destLatLng, originLatLng);
    }
        
    originMapMarker = new google.maps.Marker({ position: originLatLng });
    destinationMapMarker = new google.maps.Marker({ position: destLatLng });
    originMapMarker.setPosition(originLatLng);
    destinationMapMarker.setPosition(destLatLng);
    originMapMarker.setMap(map);
    destinationMapMarker.setMap(map);
    map.fitBounds(bounds);
}

function fitMapBounds(map, LatLngArr) {
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

        createOriginMarkerPosition();
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

        createDestinationMarkerPosition();
    });
}

function createOriginMarkerPosition() {
    let markersPosition = [];
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

    markersPosition.push(newOriginLatLng);

    if (destinationMapMarker != null) {
        currentDestinationLatLng = {
            lat: parseFloat(document.getElementById('DestinationLatitude').value),
            lng: parseFloat(document.getElementById('DestinationLongitude').value)
        };
        markersPosition.push(currentDestinationLatLng);
    }

    markersPosition.sort((a, b) => a.lng - b.lng);
    fitMapBounds(map, markersPosition);
}

function createDestinationMarkerPosition() {
    let markersPosition = [];
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

    markersPosition.push(newDestinationLatLng);

    if (originMapMarker != null) {
        currentOriginLatLng = {
            lat: parseFloat(document.getElementById('StartingLatitude').value),
            lng: parseFloat(document.getElementById('StartingLongitude').value)
        };
        markersPosition.push(currentOriginLatLng);
    }

    markersPosition.sort((a, b) => a.lng - b.lng);
    fitMapBounds(map, markersPosition);
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

//(function ($) {
//     //your standard jquery code goes here with $ prefix
//    // best used inside a page with inline code, 
//    // or outside the document ready, enter code here
//})(jQuery);

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
