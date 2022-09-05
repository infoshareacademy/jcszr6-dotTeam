// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let originLatLng, destinationLatLng;
let originMapMarker, destinationMapMarker;
let map;

function mapService() {
    $("document").ready(function () {

        originLatLng = {
            lat: parseFloat(document.getElementById("StartingLatitude").value),
            lng: parseFloat(document.getElementById("StartingLongitude").value)
        }
        destinationLatLng = {
            lat: parseFloat(document.getElementById("DestinationLatitude").value),
            lng: parseFloat(document.getElementById("DestinationLongitude").value)
        }

        initMap();
        placesAutocomplete();
        codeLatLngToFormattedAddress(originLatLng, "StartingLocation");
        codeLatLngToFormattedAddress(destinationLatLng, "DestinationLocation");
    });
}

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

            initMap(originLatLng,destinationLatLng);
            codeLatLngToFormattedAddress(originLatLng, "StartingLocation");
            codeLatLngToFormattedAddress(destinationLatLng, "DestinationLocation");
            placesAutocomplete();
        }
    );
}

function initMap(originLatLng, destLatLng) {

    function fitMapBounds(map, LatLngArr) {
        if (LatLngArr.length == 0)
            return;
        let bounds = new google.maps.LatLngBounds();
        LatLngArr.forEach(x => bounds.extend(x));
        map.fitBounds(bounds);

    }

    let bounds = new google.maps.LatLngBounds(originLatLng, destLatLng);
    let myCenter = bounds.getCenter();
    let mapOptions = { center: myCenter, zoom: 10, scrollwheel: false, draggable: true, mapTypeId: google.maps.MapTypeId.ROADMAP };
    map = new google.maps.Map(document.getElementById("map"), mapOptions);
    originMapMarker = new google.maps.Marker({ position: originLatLng });
    destinationMapMarker = new google.maps.Marker({ position: destLatLng });
    originMapMarker.setMap(map);
    destinationMapMarker.setMap(map);
    map.fitBounds(bounds);

    
}

function placesAutocomplete() {

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

    const originInput = new google.maps.places.Autocomplete(document.getElementById('StartingLocation'));
    google.maps.event.addListener(originInput, 'place_changed', function () {
        const placeDetails = originInput.getPlace();
        document.getElementById('StartingLocation').value = placeDetails.formatted_address;
        document.getElementById('StartingLatitude').value = placeDetails.geometry.location.lat();
        document.getElementById('StartingLongitude').value = placeDetails.geometry.location.lng();

        let townOrCity = GetTownOrCity(placeDetails.address_components);
        if (townOrCity)
            document.getElementById("StartingCity").value = townOrCity.long_name;

        let newOriginLatLng = new google.maps.LatLng(
            document.getElementById('StartingLatitude').value,
            document.getElementById('StartingLongitude').value);

        let currentDestinationLatLng = new google.maps.LatLng(
            document.getElementById('DestinationLatitude').value,
            document.getElementById('DestinationLongitude').value);

        originMapMarker.setPosition(newOriginLatLng);
        fitMapBounds(map, [newOriginLatLng, currentDestinationLatLng]);

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

        let currentOriginLatLng = new google.maps.LatLng(
            document.getElementById('StartingLatitude').value,
            document.getElementById('StartingLongitude').value);

        let newDestinationLatLng = new google.maps.LatLng(
            document.getElementById('DestinationLatitude').value,
            document.getElementById('DestinationLongitude').value);

        destinationMapMarker.setPosition(newDestinationLatLng);
        fitMapBounds(map, [currentOriginLatLng, newDestinationLatLng]);
    });
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
