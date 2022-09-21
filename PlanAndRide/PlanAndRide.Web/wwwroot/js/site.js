// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var originLatLng, destinationLatLng;
var originMarker, destinationMarker;
var map;
var defaultMapOptions = {
    center: {
        lat: 52.218467,
        lng: 19.134643
    }, //Poland midlle point
    zoom: 5,
    scrollwheel: false,
    draggable: true,
    mapTypeId: 'roadmap'
};
var defaultOriginMarkerLabel = { text: 'A', color: 'white' };
var defaultDestinationMarkerLabel = { text: 'B', color: 'white' };
var directionsService, directionsRenderer;
var EncodedPathElement;
var maxWaypointsNumber = 10;
var lastDirectionsResult;
function routeDetails() {
    $('document').ready(
        () => {
            originLatLng = {
                lat: parseFloat(document.getElementById("StartingLatitude").value),
                lng: parseFloat(document.getElementById("StartingLongitude").value)
            }
            destinationLatLng = {
                lat: parseFloat(document.getElementById("DestinationLatitude").value),
                lng: parseFloat(document.getElementById("DestinationLongitude").value)
            }
            map = map = new google.maps.Map(document.getElementById("map"), defaultMapOptions);
            originMarker = new google.maps.Marker({
                position: originLatLng,
                map: map,
                draggable: false,
                label: defaultOriginMarkerLabel
            });
            destinationMarker = new google.maps.Marker({
                position: destinationLatLng,
                map: map,
                draggable: false,
                label: defaultDestinationMarkerLabel
            });
            setFormattedAddressFromLatLng(originLatLng, "StartingLocation");
            setFormattedAddressFromLatLng(destinationLatLng, "DestinationLocation");
            drawRouteLine();
        }
    );
}
function createRoute() {
    $('document').ready(
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
            map = map = map = new google.maps.Map(document.getElementById("map"), defaultMapOptions);
            directionsService = new google.maps.DirectionsService();
            directionsRenderer = new google.maps.DirectionsRenderer({
                map: map,
                draggable: true
            });
            originMarker = new google.maps.Marker({
                map: null,
                draggable: true,
                visible: true,
                label: defaultOriginMarkerLabel
            });
            destinationMarker = new google.maps.Marker({
                map: null,
                draggable: true,
                visible: true,
                label: defaultDestinationMarkerLabel
            });
            originMarker.addListener("dragend", originMarkerPositionChangeHandler);
            destinationMarker.addListener("dragend", destinationMarkerPositionChangeHandler);
            setPlacesListener(originElementsId, destinationElementsId);
        }
    );
}
function editRoute() {
    $('document').ready(
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
            setFormattedAddressFromLatLng(originLatLng, "StartingLocation");
            setFormattedAddressFromLatLng(destinationLatLng, "DestinationLocation");
            map = map = map = new google.maps.Map(document.getElementById("map"), defaultMapOptions);
            directionsService = new google.maps.DirectionsService();
            directionsRenderer = new google.maps.DirectionsRenderer({
                map: map,
                draggable: true
            });
            originMarker = new google.maps.Marker({
                map: map,
                position: originLatLng,
                draggable: true,
                visible: false,
                label: defaultOriginMarkerLabel
            });
            destinationMarker = new google.maps.Marker({
                map: map,
                position: destinationLatLng,
                draggable: true,
                visible: false,
                label: defaultDestinationMarkerLabel
            });
            originMarker.addListener("dragend", originMarkerPositionChangeHandler);
            destinationMarker.addListener("dragend", destinationMarkerPositionChangeHandler);
            setPlacesListener(originElementsId, destinationElementsId);
            drawDirectionsLine();
        }
    );
}
function calcDirections() {
    if (originMarker.getMap() == null || destinationMarker.getMap() == null)
        return;

    let start = originMarker.getPosition();
    let end = destinationMarker.getPosition();
    let waypoints = [];
    let encodedWaypoints = document.getElementById("EncodedWaypoints").value;
    if (encodedWaypoints != "") {
        waypoints = decodeWaypoints(encodedWaypoints);
    }

    let request = {
        origin: start,
        destination: end,
        travelMode: 'DRIVING',
        waypoints: waypoints
    };

    directionsService.route(request, function (result, status) {
        if (status == 'OK') {
            originMarker.setVisible(false);
            destinationMarker.setVisible(false);
            directionsRenderer.setDirections(result);
            document.getElementById("EncodedPath").value = result.routes[0].overview_polyline;
            directionsRenderer.addListener("directions_changed", directionsChangeHandler);
            lastDirectionsResult = result;
        }
        else {
            console.log("Direction service response status: " + status);
        }
    });
    fitMap(map, [originMarker.getPosition(), destinationMarker.getPosition()]);
}
function directionsChangeHandler() {
    let result = directionsRenderer.getDirections();
    document.getElementById("EncodedPath").value = result.routes[0].overview_polyline;

    let directionsStartPosition = result.routes[0].legs[0].start_location;
    let legsCount = result.routes[0].legs.length;
    let directionsEndPosition = result.routes[0].legs[legsCount - 1].end_location;
    if (!directionsStartPosition.equals(originMarker.getPosition())) {
        setOriginMarkerPosition(directionsStartPosition, false);
    }
    if (!directionsEndPosition.equals(destinationMarker.getPosition())) {
        setDestinationMarkerPosition(directionsEndPosition, false);
    }

    let waypoints = result.request.waypoints;
    if (waypoints.length > maxWaypointsNumber) {
        alert("You can add up to " + maxWaypointsNumber + " waypoints.");
        directionsRenderer.setDirections(lastDirectionsResult);
        return;
    }
    lastDirectionsResult = result;
    let encodedWaypoints = encodeWaypoints(waypoints);
    document.getElementById("EncodedWaypoints").value = encodedWaypoints;
}
function encodeWaypoints(directionsWaypointArr) {
    let latLngArr = directionsWaypointArr.map(x => x.location);
    return google.maps.geometry.encoding.encodePath(latLngArr);
}
function decodeWaypoints(encodedWaypoints) {
    let decodedLatLng = google.maps.geometry.encoding.decodePath(encodedWaypoints);
    let waypoints = decodedLatLng.map(x => {
        return {
            location: x,
            stopover: false
        };
    });
    return waypoints;
}
function decodeRoutePolyline() {
    let encodedPath = document.getElementById("EncodedPath").value;
    if (encodedPath == "") {
        return [];
    }
    return google.maps.geometry.encoding.decodePath(encodedPath);
}
function fitMap(map, boundsArr) {
    if (boundsArr.length == 0)
        return;
    if (boundsArr.length == 1) {
        map.setCenter(boundsArr[0]);
        map.setZoom(10);
        return;
    }
    let bounds = new google.maps.LatLngBounds();
    boundsArr.sort((a, b) => a.lng - b.lng);
    boundsArr.forEach(x => bounds.extend(x));
    map.fitBounds(bounds);
}
function drawRouteLine() {
    let decodedPath = decodeRoutePolyline();
    let lineOptions = {
        path: decodedPath,
        geodesic: true,
        strokeColor: "#0066ff",
        strokeOpacity: 0.7,
        strokeWeight: 5,
    }
    let polyline = new google.maps.Polyline(lineOptions);
    polyline.setMap(map);
    fitMap(map, decodedPath);
}
function drawDirectionsLine() {
    calcDirections();
}
function getTownOrCity(addressComponent) {
    const Intersect = function (a, b) {
        return new Set(a.filter(v => ~b.indexOf(v)));
    };
    if (typeof (addressComponent) == 'object' && addressComponent instanceof Array) {

        let order = ['locality', 'administrative_area_level_2', 'administrative_area_level_1'];

        for (let i = 0; i < addressComponent.length; i++) {
            let obj = addressComponent[i];
            let types = obj.types;
            if (Intersect(order, types).size > 0) return obj;
        }
    }
    return false;
};
function setPlacesListener(origin, destination) {
    const originAddressInput = new google.maps.places.Autocomplete(document.getElementById(origin.addressElementId));
    google.maps.event.addListener(originAddressInput, 'place_changed', function () {
        const placeDetails = originAddressInput.getPlace();
        document.getElementById(origin.addressElementId).value = placeDetails.formatted_address;
        let townOrCity = getTownOrCity(placeDetails.address_components);
        if (townOrCity) {
            document.getElementById(origin.cityElementId).value = townOrCity.long_name;
        }
        setOriginMarkerPosition(placeDetails.geometry.location, true);
    });
    const destinationAddressInput = new google.maps.places.Autocomplete(document.getElementById(destination.addressElementId));
    google.maps.event.addListener(destinationAddressInput, 'place_changed', function () {
        const placeDetails = destinationAddressInput.getPlace();
        document.getElementById(destination.addressElementId).value = placeDetails.formatted_address;
        let townOrCity = getTownOrCity(placeDetails.address_components);
        if (townOrCity) {
            document.getElementById(destination.cityElementId).value = townOrCity.long_name;
        }
        setDestinationMarkerPosition(placeDetails.geometry.location, true);
    });
}
function setOriginMarkerPosition(latLng, checkNewDirections) {
    google.maps.event.addListenerOnce(originMarker, "position_changed", originMarkerPositionChangeHandler);
    if (originMarker.getMap() == null) {
        originMarker.setMap(map);
        originMarker.setPosition(latLng);
    }
    else {
        originMarker.setPosition(latLng);
    }
    if (checkNewDirections && destinationMarker.getMap() != null) {
        calcDirections();
    }
}
function originMarkerPositionChangeHandler() {
    let markersLatLng = [];
    let newOriginLatLng = originMarker.getPosition();
    document.getElementById('StartingLatitude').value = newOriginLatLng.lat();
    document.getElementById('StartingLongitude').value = newOriginLatLng.lng();
    setFormattedAddressFromLatLng(newOriginLatLng, 'StartingLocation');
    setCityOrTownFromLatLng(newOriginLatLng, 'StartingCity');
    markersLatLng.push(newOriginLatLng);
    if (destinationMarker.getMap() != null) {
        markersLatLng.push(destinationMarker.getPosition());
    }
    fitMap(map, markersLatLng);
}
function setDestinationMarkerPosition(latLng, checkNewDirections) {
    google.maps.event.addListenerOnce(destinationMarker, "position_changed", destinationMarkerPositionChangeHandler);
    if (destinationMarker.getMap() == null) {
        destinationMarker.setMap(map);
        destinationMarker.setPosition(latLng);
    }
    else {
        destinationMarker.setPosition(latLng);
    }
    if (checkNewDirections && originMarker.getMap() != null) {
        calcDirections()
    }
}
function destinationMarkerPositionChangeHandler() {
    let markersLatLng = [];
    let newDestinationLatLng = destinationMarker.getPosition();
    document.getElementById('DestinationLatitude').value = newDestinationLatLng.lat();
    document.getElementById('DestinationLongitude').value = newDestinationLatLng.lng();
    setFormattedAddressFromLatLng(newDestinationLatLng, 'DestinationLocation');
    setCityOrTownFromLatLng(newDestinationLatLng, 'DestinationCity');
    markersLatLng.push(newDestinationLatLng);
    if (originMarker.getMap() != null) {
        markersLatLng.push(originMarker.getPosition());
    }
    fitMap(map, markersLatLng);
}
function setFormattedAddressFromLatLng(latLng, addressElementId) {
    const geocoder = new google.maps.Geocoder();
    geocoder.geocode({ location: latLng })
        .then((response) => {
            if (response.results[0]) {
                document.getElementById(addressElementId).value = response.results[0].formatted_address;
            }
        })
        .catch((e) => {
            console.log("Geocoder failed due to: " + e);
            document.getElementById(addressElementId).value = "";
        });
}
function setCityOrTownFromLatLng(latLng, idCityOrTownEl) {
    const geocoder = new google.maps.Geocoder();
    geocoder.geocode({ location: latLng })
        .then((response) => {
            if (response.results[0]) {
                let townOrCity = getTownOrCity(response.results[0].address_components);
                if (townOrCity) {
                    document.getElementById(idCityOrTownEl).value = townOrCity.long_name;
                }
            }
        })
        .catch((e) => {
            console.log("Geocoder failed due to: " + e);
            document.getElementById(idCityOrTownEl).value = "";
        });
        
}

function initRaitingDisplay() {
    $(".route-rating").starRating({
        starSize: 25,
        initialRating: $("#rating-value").val(),
        readOnly: true,
    });
}

function countDescriptionCharacters() {
    var max = $(this).attr("maxlength");
    var length = $(this).val().length;
    var counter = max - length;
    var helper = $(this).next(".form-text");
    // Switch to the singular if there's exactly 1 character remaining
    if (counter !== 1) {
        helper.text(counter + " characters remaining");
    } else {
        helper.text(counter + " character remaining");
    }
    // Make it red if there are 0 characters remaining
    if (counter === 0) {
        helper.removeClass("text-muted");
        helper.addClass("text-danger");
    } else {
        helper.removeClass("text-danger");
        helper.addClass("text-muted");
    }
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
