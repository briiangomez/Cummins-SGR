function fullScreenFunct() {
    var el = document.getElementById("newMap");
    if (el.requestFullscreen) {
        el.requestFullscreen();
        $(document).on("fullscreenchange", fullScreenExit);
    }
    else if (el.mozRequestFullScreen) {
        el.mozRequestFullScreen();
        $(document).on("mozfullscreenchange", fullScreenExit);
    }
    else if (el.webkitRequestFullScreen) {
        el.webkitRequestFullScreen();
        $(document).on("webkitfullscreenchange", fullScreenExit);
    }
    map.setOptions({ height: MapSize.fullScreen.height, width: MapSize.fullScreen.width });
}

function fullScreenExit() {
    if (!isFullScreen())
        map.setOptions({ height: MapSize.normalSize.height, width: MapSize.normalSize.width });
}

function isFullScreen() {
    return document.fullscreen || document.webkitIsFullScreen || document.mozFullScreen;
}

function CustomConfirm(titulo, mensaje, tipo)
{
    return new Promise(resolve => {
        Swal.fire({
            title: titulo,
            text: mensaje,
            icon: tipo,
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Confirmar',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.value) {
                resolve(true);
            }
            else {
                resolve(false);
            }
        });
    });
}

var miMapa;

function generarPin(lat, long, zoom, MisAutos) {

    MiMapa = new Microsoft.Maps.Map('#newMap', {});

    MiMapa.setView({
        mapTypeId: Microsoft.Maps.MapTypeId.road,
        center: new Microsoft.Maps.Location(lat, long),
        zoom: zoom
    });

    Microsoft.Maps.loadModule('Microsoft.Maps.DrawingTools', function () {
        //se declara tools como una nueva instancia de las herramientas de dibujo
        var tools = new Microsoft.Maps.DrawingTools(MiMapa);

        //mostrar barra de herramientas para editar
        tools.showDrawingManager(function (manager) {
            //se le da una referencia al drawing manager
            drawingManager = manager;
        });

    });

    MisAutos.forEach(auto => AgregarNuevoPin(auto)); //Agregar los autos al mapa apartir del array
}

function AgregarNuevoPin(auto) {
    let localizar = new Microsoft.Maps.Location(auto.lat, auto.long);

    let pushPin = new Microsoft.Maps.Pushpin(localizar, {
        color: "red",
        text: auto.name
    });

    MiMapa.entities.push(pushPin); ///poner los pines dentro del mapa
}

function limpiarPin(id) {
    for (var i = MiMapa.entities.getLength() - 1; i >= 0; i--) {
        var pushpin = MiMapa.entities.get(i);

        if (pushpin.entity.iconText == id.toString()) {
            MiMapa.entities.removeAt(i);
        }
    }
}

function LimpiarAgregarPint(id, auto) {
    limpiarPin(id);
    AgregarNuevoPin(auto);
}

//funcion para agregar pines al mapa segun el objeto auto 


///EDITAR MAPAS

function editarMisMapas() {
    var map, baseLayer, drawingManager;


    map = new Microsoft.Maps.Map('#miMapa', {});

    map.setView({
        mapTypeId: Microsoft.Maps.MapTypeId.road,
        center: new Microsoft.Maps.Location(-34.9767433117713, -58.18140713435604),
        zoom: 16
    });

    //Cargar el modulo de DrawingTools
    Microsoft.Maps.loadModule('Microsoft.Maps.DrawingTools', function () {
        //crear el baselayer para hacer dibujos de poligonos
        baseLayer = new Microsoft.Maps.Layer();

        //evento click al baselayer
        Microsoft.Maps.Events.addHandler(baseLayer, 'click', function (e) {
            console.log("hiciste click");
            console.log(baseLayer._primitives[0]);
        });

        map.layers.insert(baseLayer);

        //Create an instance of the DrawingTools class and bind it to the map.
        var tools = new Microsoft.Maps.DrawingTools(map);

        //Show the drawing toolbar and enable editting on the map.
        tools.showDrawingManager(function (manager) {
            drawingManager = manager;

            Microsoft.Maps.Events.addHandler(drawingManager, 'drawingEnded', function (e) {
                //When use finisihes drawing a shape, removing it from the drawing layer and add it to the base layer.
                moveShapesBetweenLayers(drawingManager, baseLayer);
            });

            Microsoft.Maps.Events.addHandler(drawingManager, 'drawingModeChanged', function (e) {
                //When the mode changes to edit or erase, move all shapes in the baseLayer to the drawing layer.
                if (e === Microsoft.Maps.DrawingTools.DrawingMode.edit || e === Microsoft.Maps.DrawingTools.DrawingMode.erase) {
                    moveShapesBetweenLayers(baseLayer, drawingManager);
                }
            });
        })
    });


    function moveShapesBetweenLayers(fromLayer, toLayer) {
        var shapes = fromLayer.getPrimitives();

        if (shapes) {
            fromLayer.clear();
            toLayer.add(shapes);
        }
    }
}

function AutoCompleteBing(id)
{
    $("#" + id).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "https://dev.virtualearth.net/REST/v1/Locations",
                dataType: "jsonp",
                data: {
                    key: "Au6FtOLBvJ2d7663z152CpnTsWA5Mq234QwqCLxJ-UkX-O7z91HwOEb-MbjXNbMo",
                    addressLine: request.term,
                    CountryRegion: "AR"
                },
                jsonp: "jsonp",
                success: function (data) {
                    var result = data.resourceSets[0];
                    if (result) {
                        if (result.estimatedTotal > 0) {
                            response($.map(result.resources, function (item) {
                                return {
                                    data: item,
                                    label: item.name + ' (' + item.address.countryRegion + ')',
                                    value: item.name
                                }
                            }));
                        }
                    }
                }
            });
        },
        minLength: 1,
        change: function (event, ui) {
            if (!ui.item)
                $("#" + id).val('');
        },
        select: function (event, ui) {
            document.getElementById("Lat").value = ui.item.data.point.coordinates[0];
            document.getElementById("Long").value = ui.item.data.point.coordinates[1];
            document.getElementById("Dir").value = ui.item.data.name;
            MiMapa = new Microsoft.Maps.Map('#newMap', {});

            MiMapa.setView({
                mapTypeId: Microsoft.Maps.MapTypeId.road,
                center: new Microsoft.Maps.Location(ui.item.data.point.coordinates[0], ui.item.data.point.coordinates[1]),
                zoom: 16
            });

            Microsoft.Maps.loadModule('Microsoft.Maps.DrawingTools', function () {
                //se declara tools como una nueva instancia de las herramientas de dibujo
                var tools = new Microsoft.Maps.DrawingTools(MiMapa);

                //mostrar barra de herramientas para editar
                tools.showDrawingManager(function (manager) {
                    //se le da una referencia al drawing manager
                    drawingManager = manager;
                });

            });

            let localizar = new Microsoft.Maps.Location(ui.item.data.point.coordinates[0], ui.item.data.point.coordinates[1]);

            let pushPin = new Microsoft.Maps.Pushpin(localizar, {
                color: "red",
                text: ui.item.data.name
            });

            MiMapa.entities.push(pushPin); ///poner los pines dentro del mapa
        }
    });
}

function addPin(lat, long,direcc)
{
    console.log(lat);
    console.log(long);
    document.getElementById("Lat").value = lat;
    document.getElementById("Long").value = long;
    document.getElementById("Dir").value = direcc
    MiMapa = new Microsoft.Maps.Map('#newMap', {});

    MiMapa.setView({
        mapTypeId: Microsoft.Maps.MapTypeId.road,
        center: new Microsoft.Maps.Location(lat,long),
        zoom: 16
    });

    Microsoft.Maps.loadModule('Microsoft.Maps.DrawingTools', function () {
        //se declara tools como una nueva instancia de las herramientas de dibujo
        var tools = new Microsoft.Maps.DrawingTools(MiMapa);

        //mostrar barra de herramientas para editar
        tools.showDrawingManager(function (manager) {
            //se le da una referencia al drawing manager
            drawingManager = manager;
        });

    });

    let localizar = new Microsoft.Maps.Location(lat,long);

    let pushPin = new Microsoft.Maps.Pushpin(localizar, {
        color: "red",
        text: direcc
    });

    MiMapa.entities.push(pushPin); ///poner los pines dentro del mapa
}

function getValue(id)
{
    return document.getElementById(id).value;
}

function setValue(id, valor) {
    document.getElementById(id).value = valor;
}

function downloadFile(mimeType, base64String, fileName) {

    var fileDataUrl = "data:" + mimeType + ";base64," + base64String;
    fetch(fileDataUrl)
        .then(response => response.blob())
        .then(blob => {

            //create a link
            var link = window.document.createElement("a");
            link.href = window.URL.createObjectURL(blob, { type: mimeType });
            link.download = fileName;

            //add, click and remove
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        });

}
