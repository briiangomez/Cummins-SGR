window.googleAddressLookup = {


initAutocomplete : function () {
    autocomplete = new google.maps.places.Autocomplete(
        document.getElementById('autocomplete'), { types: ['geocode'] });

    autocomplete.setFields(['address_component', 'geometry']);

    autocomplete.addListener('place_changed', fillInAddress);
},

fillInAddress : function () {
    var place = autocomplete.getPlace();

    document.getElementById('autocomplete').id = 'street_number';

    for (var component in componentForm) {
        document.getElementById(component).value = '';
        document.getElementById(component).disabled = false;
    }

    document.getElementById("hidden_lat_input").value = place.geometry.location.lat();
    document.getElementById("hidden_lng_input").value = place.geometry.location.lng();

    for (var i = 0; i < place.address_components.length; i++) {
        var addressType = place.address_components[i].types[0];
        if (componentForm[addressType]) {
            var val = place.address_components[i][componentForm[addressType]];
            document.getElementById(addressType).value = val;
        }
    }

    document.getElementById('street_number').id = 'autocomplete';
},

geolocate: function () {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var geolocation = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };
            var circle = new google.maps.Circle(
                { center: geolocation, radius: position.coords.accuracy });
            autocomplete.setBounds(circle.getBounds());
        });
    }
}
}