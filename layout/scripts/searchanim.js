//A java-script to display a nice animation when loading the page.
$(document).ready(load);
		
function load()
{
	//style = window.getComputedStyle(search),
    //h = style.getPropertyValue('height');
	$(search).css({left: "-100px", opacity: 0});
	//$(search).css({"padding-top": 0});
	$(search).animate({ left: "-10px", opacity: 1 }, 500);
	$("div.wrapper.col2").css({width: screen.width-50})
}