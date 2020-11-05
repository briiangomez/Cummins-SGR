window.Parcial2MapComp = {

    setMappointData: function(titulo, dataserie) {

        function pointsToPath(from, to, invertArc) {
            var arcPointX = (from.x + to.x) / (invertArc ? 2.4 : 1.6),
                arcPointY = (from.y + to.y) / (invertArc ? 2.4 : 1.6);
            return 'M' + from.x + ',' + from.y + 'Q' + arcPointX + ' ' + arcPointY +
                ',' + to.x + ' ' + to.y;
        }


        var inifin = [];
        inifin.push(dataserie[0]);
        var rest = dataserie.slice(1, dataserie.length-1);

        var chart = Highcharts.mapChart('mapcontainer', {

            chart: {
                map: 'countries/ar/ar-all'
            },

            title: {
                text: titulo
            },

            legend: {
                align: 'left',
                layout: 'vertical',
                floating: true
            },

            mapNavigation: {
                enabled: true
            },

            tooltip: {
                headerFormat: '',
                pointFormat: '<b>{point.id}</b>'//'<b>{point.name}</b><br>Lat: {point.lat}, Lon: {point.lon}'
            },

            //plotOptions: {
            //    series: {
            //        marker: {
            //            fillColor: '#FFFFFF',
            //            lineWidth: 2,
            //            lineColor: Highcharts.getOptions().colors[1]
            //        }
            //    }
            //},

            series:
                [{
                    // Use the gb-all map with no data as a basemap
                    name: 'Basemap',
                    borderColor: '#A0A0A0',
                    nullColor: 'rgba(200, 200, 200, 0.3)',
                    showInLegend: false
                },
                {
                    name: 'Separators',
                    type: 'mapline',
                    nullColor: '#707070',
                    showInLegend: false,
                    enableMouseTracking: false
                },
                {
                    // Specify points using lat/lon
                    type: 'mappoint',
                    name: 'Partida-Llegada',
                    //color: Highcharts.getOptions().colors[1],
                    data: inifin, //dataserie
                    marker: {
                        enabled: true,
                        symbol: "circle",
                        radius: 10
                    }
                },
                {
                    // Specify points using lat/lon
                    type: 'mappoint',
                    name: 'Recorrido',
                    //color: Highcharts.getOptions().colors[1],
                    data: rest, //dataserie //[{ name: 'Ciudad Autonoma', lat: -34.6, lon: -58.45}]
                    marker: {
                        enabled: true,
                        symbol: "circle",
                        radius: 5
                    }
                }
                ]
        });

        var i;
        var linesserie = [];
        for (i = 0; i < dataserie.length - 1; i++) {

            var fromPoint = chart.get(dataserie[i].id);
            var toPoint = chart.get(dataserie[i + 1].id);
            var data = {};
            data.id = dataserie[i].id + ' - ' + dataserie[i + 1].id;
            data.path = pointsToPath(fromPoint, toPoint);
            data.color = dataserie[i].color; //'#85929E';
            linesserie.push(data);
        }

        //linesserie[0].color = '#5DADE2';
        //linesserie[linesserie.length - 1].color = '#5DADE2';

        chart.addSeries({
            name: 'Rutas',
            type: 'mapline',
            lineWidth: 3,
            color: Highcharts.getOptions().colors[1],
            data: linesserie
        });

        //chart.addSeries({
        //    name: 'Rutas',
        //    type: 'mapline',
        //    lineWidth: 3,
        //    color: Highcharts.getOptions().colors[1],
        //    data: inifin,
        //    marker: {
        //        enabled: true,
        //        symbol: "circle",
        //        radius: 50
        //    }
        //});

    }
};
