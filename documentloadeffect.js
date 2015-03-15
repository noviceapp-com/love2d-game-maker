//A java-script to display a nice animation when loading the page.
$(document).ready(load);
		
function load()
{
	style = window.getComputedStyle(mainContent),
    h = style.getPropertyValue('height');
	$(mainContent).css({height: 0});
	$(mainContent).css({"padding-top": 0});
	$(mainContent).animate({height: h,"padding-top": 20},700);
}