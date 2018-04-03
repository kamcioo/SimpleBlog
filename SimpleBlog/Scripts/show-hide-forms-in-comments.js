function toggle(className, idToShow) {
    var elements = document.getElementsByClassName(className)
    for (var i = 0; i < elements.length; i++) {
        elements[i].style.display = 'none';
    }
    var elementToShow = document.getElementById(idToShow)
    elementToShow.style.display = "block";
    elementToShow.scrollIntoView(false);
}

function scrollIntoId(id) {
    var item = document.getElementById(id);
    if (!!item && item.scrollIntoView) {
        item.scrollIntoView(true);
        window(scrollBy(0, -100));
    }
}