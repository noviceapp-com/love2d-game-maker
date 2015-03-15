//The main-page's javascript animating everything.
$(document).ready(load);

function load()
{	
	$("h1").css({ opacity: 0})
	$("p.intro").css({ opacity: 0})
	
	function fadeinintroh1()
	{
		return function(){ $("h1").animate({opacity: 1},1000); }
	}
	function fadeinintrop()
	{
		return function(){ $("p.intro").animate({opacity: 1},1700); }
	}
	
	setTimeout(fadeinintroh1(), 300);
	setTimeout(fadeinintrop(), 1300);
	animateintrobg();
}

function animateintrobg()
{
	$(intro).css({opacity: 0});
	i = Math.floor(Math.random() * 2);
	
	switch (i)
	{
		case 0:
			changeintrobg("images/introbg.png");
			break;
		case 1:
			changeintrobg("images/introbg2.png");
			break;
	}
	
	$(intro).animate({opacity: 1},1000);
	//setTimeout(function(){$(info).animate;window.alert("works")},20);
	//setTimeout(animateintrobg, 1300);
}

function changeintrobg(newbg)
{
	$(intro).css("background", "url('"+newbg+"')");
}