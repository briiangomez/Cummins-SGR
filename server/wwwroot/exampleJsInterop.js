window.exampleJsFunctions = {
    showPrompt: function (text) {
        return prompt(text, 'Escriba su nombre');
    },
    displayWelcome: function (welcomeMessage) {
        document.getElementById('welcome').innerText = welcomeMessage;
    }
};