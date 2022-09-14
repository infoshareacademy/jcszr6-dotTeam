// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var originLatLng, destinationLatLng;
var originMarker, destinationMarker;
var map;
var directionsService, directionsRenderer;
var EncodedPathElement;
var maxWaypointsNumber = 10;
var lastDirectionsResult;

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

            initMap(originLatLng, destinationLatLng);
            codeLatLngToFormattedAddress(originLatLng, "StartingLocation");
            codeLatLngToFormattedAddress(destinationLatLng, "DestinationLocation");
            drawRouteLine();
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
            directionsRenderer.setOptions({ draggable: true });
            codeLatLngToFormattedAddress(originLatLng, "StartingLocation");
            codeLatLngToFormattedAddress(destinationLatLng, "DestinationLocation");
            setPlacesListenerAndRenderMapOnChange(originElementsId, destinationElementsId);
            drawDirectionsLine();
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
            setPlacesListenerAndRenderMapOnChange(originElementsId, destinationElementsId);
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
    //setOriginMarkerPosition(originLatLng);
    //setDestinationMarkerPosition(destLatLng);
    originMarker = new google.maps.Marker({
        position: originLatLng,
        map: map,
        draggable: true
    });
    destinationMarker = new google.maps.Marker({
        position: destLatLng,
        map: map,
        draggable: true
    });
    //originMarker.setPosition(originLatLng);
    //destinationMarker.setPosition(destLatLng);
    //originMarker.setMap(map);
    //destinationMarker.setMap(map);
    //originMarker.addListener("position_changed", originMarkerPositionChangeHandler);
    //destinationMarker.addListener("position_changed", destinationMarkerPositionChangeHandler);
}
function calcRoute() {
    if (originMarker == null || destinationMarker == null)
        return;

    let start = originMarker.getPosition();
    let end = destinationMarker.getPosition();
    let waypoints = [];
    let encodedWaypoints = document.getElementById("EncodedWaypoints").value;
    if (encodedWaypoints != ""){
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
        originMarker.addListener("position_changed", originMarkerPositionChangeHandler);
        setOriginMarkerPosition(directionsStartPosition);
    }
    if (!directionsEndPosition.equals(destinationMarker.getPosition())) {
        destinationMarker.addListener("position_changed", destinationMarkerPositionChangeHandler);
        setDestinationMarkerPosition(directionsEndPosition);
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
    if (encodedPath == "")
        return [];

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
        strokeColor: "#FF0000",
        strokeOpacity: 1,
        strokeWeight: 3,
    }
    let polyline = new google.maps.Polyline(lineOptions);
    polyline.setMap(map);
    originMarker.setDraggable(false);
    destinationMarker.setDraggable(false);
    fitMap(map, decodedPath);
}
function drawDirectionsLine() {
    calcRoute();
}
function getTownOrCity (addressComponent) {
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
function setPlacesListenerAndRenderMapOnChange(origin, destination) {

    

    const originAddressInput = new google.maps.places.Autocomplete(document.getElementById(origin.addressElementId));
    google.maps.event.addListener(originAddressInput, 'place_changed', function () {
        const placeDetails = originAddressInput.getPlace();
        document.getElementById(origin.addressElementId).value = placeDetails.formatted_address;
        //document.getElementById(origin.latElementId).value = placeDetails.geometry.location.lat();
        //document.getElementById(origin.lngElementId).value = placeDetails.geometry.location.lng();

        let townOrCity = getTownOrCity(placeDetails.address_components);
        if (townOrCity)
            document.getElementById(origin.cityElementId).value = townOrCity.long_name;

        setOriginMarkerPosition(placeDetails.geometry.location);
        calcRoute();

    });
    const destinationAddressInput = new google.maps.places.Autocomplete(document.getElementById(destination.addressElementId));
    google.maps.event.addListener(destinationAddressInput, 'place_changed', function () {
        const placeDetails = destinationAddressInput.getPlace();
        document.getElementById(destination.addressElementId).value = placeDetails.formatted_address;
        //document.getElementById(destination.latElementId).value = placeDetails.geometry.location.lat();
        //document.getElementById(destination.lngElementId).value = placeDetails.geometry.location.lng();

        let townOrCity = getTownOrCity(placeDetails.address_components);
        if (townOrCity)
            document.getElementById(destination.cityElementId).value = townOrCity.long_name;

        setDestinationMarkerPosition(placeDetails.geometry.location);
        calcRoute();
    });
}
function setOriginMarkerPosition(latLng) {


    if (originMarker == null) {
        originMarker = new google.maps.Marker(
            {
                //position: latLng,
                title: "START",
                draggable: true
            });
        originMarker.setMap(map);
        originMarker.addListener("drag_end", originMarkerPositionChangeHandler);
        originMarker.setPosition(latLng);
    }
    else {
        //originMarker.removeListener("position_changed", originMarkerPositionChangeHandler);

        originMarker.setPosition(latLng);
        
    }
}
function originMarkerPositionChangeHandler() {
    let markersLatLng = [];
    let newOriginLatLng = originMarker.getPosition();
    document.getElementById('StartingLatitude').value = newOriginLatLng.lat();
    document.getElementById('StartingLongitude').value = newOriginLatLng.lng();
    codeLatLngToFormattedAddress(newOriginLatLng, 'StartingLocation');
    codeLatLngToCityOrTown(newOriginLatLng, 'StartingCity');
    markersLatLng.push(newOriginLatLng);

    if (destinationMarker != null) {
        markersLatLng.push(destinationMarker.getPosition());
    }

    fitMap(map, markersLatLng);
}
function setDestinationMarkerPosition(latLng) {
    if (destinationMarker == null) {
        destinationMarker = new google.maps.Marker(
            {
                //position: latLng,
                title: "END",
                draggable: true
            });
        destinationMarker.setMap(map);
        destinationMarker.addListener("drag_end", destinationMarkerPositionChangeHandler);
        destinationMarker.setPosition(latLng);
    }
    else {
        //destinationMarker.removeListener("position_changed", destinationMarkerPositionChangeHandler);
        destinationMarker.setPosition(latLng);
        
    }
}
function destinationMarkerPositionChangeHandler() {
    let markersLatLng = [];
    let newDestinationLatLng = destinationMarker.getPosition();

    document.getElementById('DestinationLatitude').value = newDestinationLatLng.lat();
    document.getElementById('DestinationLongitude').value = newDestinationLatLng.lng();
    codeLatLngToFormattedAddress(newDestinationLatLng, 'DestinationLocation');
    codeLatLngToCityOrTown(newDestinationLatLng, 'DestinationCity');
    markersLatLng.push(newDestinationLatLng);

    if (originMarker != null) {
        markersLatLng.push(originMarker.getPosition());
    }

    fitMap(map, markersLatLng);
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
function codeLatLngToCityOrTown(latLng, idCityOrTownEl) {
    const geocoder = new google.maps.Geocoder();
    geocoder.geocode({ location: latLng }).then
        ((response) => {
            if (response.results[0]) {
                let townOrCity = getTownOrCity(response.results[0].address_components);
                if (townOrCity) {
                    document.getElementById(idCityOrTownEl).value = townOrCity.long_name;
                }
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
