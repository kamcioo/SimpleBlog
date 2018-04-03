$(document).ready(function () {
    setInterval(changeElements, 3000);
});

var strong = $("header.intro-header strong");
var elements = ["MVC", ".NET", "JS", "CSS", "HTML", "JQUERY"];
var counter = 0;

function changeElements() {
    strong.fadeOut(800, function(){
        strong.text(elements[counter]);
    });
    strong.fadeIn(800, function () {
        
    });
    counter++;
    if (counter >= elements.length) {
        counter = 0;
    }

}