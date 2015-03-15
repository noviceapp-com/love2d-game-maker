//The main-page's javascript animating everything.
$(document).ready(load);
introbgid = 0;

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
	
	$(intro).css({opacity: 0});
	$(intro).animate({opacity: 1},1000);
	setTimeout(animateintrobg,4000);
}

function animateintrobg()
{
	if (introbgid < 1){introbgid++;}else{introbgid=0;}
	switch (introbgid)
	{
		case 0:
			changeintrobg("images/introbg.png");
			break;
		case 1:
			changeintrobg("images/introbg2.png");
			break;
	}
	setTimeout(animateintrobg,4000);
	//setTimeout(animateintrobg, 1300);
}

function changeintrobg(newbg)
{
	$(intro).css("background", "url('"+newbg+"') no-repeat center");
}