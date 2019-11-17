window.getDimensions = function () {
    return {
        width: window.innerWidth,
        height: window.innerHeight
    };
};

window.onresize = () => {

    window.DotNet.invokeMethodAsync(
        'Pacman.Core', 'UpdateWindowSize', window.innerWidth, window.innerHeight
    );
};

window.JsInteropWindow = {

    initialize: () => {
        window.DotNet.invokeMethodAsync(
            'Pacman.Core', 'WindowInitialized', window.innerWidth, window.innerHeight
        );
    }
};