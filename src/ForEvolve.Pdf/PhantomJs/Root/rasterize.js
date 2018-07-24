'use strict';
var page = require('webpage').create();
var system = require('system');

if (system.args.length != 4) {
    console.log('Usage: rasterize.js URI filename jsonOptions');
    console.log('  jsonOptions: webpage module json serialized data.');
    console.log(
        '    Supported properties: paperSize, viewportSize, zoomFactor and clipRect.'
    );
    console.log('    Example: ');
    console.log('    {');
    console.log('      "paperSize":{');
    console.log('        "format":"Letter",');
    console.log('        "orientation":"portrait",');
    console.log('        "margin":{');
    console.log('          "top":"1in",');
    console.log('          "right":"1in",');
    console.log('          "bottom":"1in",');
    console.log('          "left":"1in"');
    console.log('        }');
    console.log('      },');
    console.log('      "viewportSize":{"width":600,"height":600},');
    console.log('      "zoomFactor":1.0');
    console.log('    }');
    phantom.exit(1);
} else {
    var uri = system.args[1];
    var outputFileName = system.args[2];
    var options = JSON.parse(system.args[3]);
    if (options.paperSize) {
        page.paperSize = options.paperSize;
    }
    if (options.viewportSize) {
        page.viewportSize = options.viewportSize;
    }
    if (options.zoomFactor) {
        page.zoomFactor = options.zoomFactor;
    }
    if (options.clipRect) {
        page.clipRect = options.clipRect;
    }

    page.open(uri, function(status) {
        if (status !== 'success') {
            console.error('Unable to load the URI. Status: ' + status);
            phantom.exit(1);
        } else {
            page.render(outputFileName);
            phantom.exit();
        }
    });
}
