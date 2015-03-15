//The main-page's javascript animating everything.
$(document).ready(load);
introbgid = 0;
animatingintro = false;

function load()
{	
	$("h1").css({ opacity: 0});
	$("p.intro").css({ opacity: 0});
	$(".dot").click(changedot);
	$(".selecteddot").click(changedot);
	
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

function animateintrobg(dontadd)
{
	if (!animatingintro)
	{
		animatingintro = true;
		if ((!dontadd || dontadd == undefined || dontadd == null) && introbgid < 1){console.log('adding');introbgid++;}else if(!dontadd){console.log('adding2');introbgid=0;}
		switch (introbgid)
		{
			case 0:
				changeintrobg("images/introbg.png");
				break;
			case 1:
				changeintrobg("images/introbg2.png");
				break;
		}
		animatingintro = false;
		if (!dontadd)
		{
			setTimeout(animateintrobg,4000);
			
			$(".selecteddot").toggleClass("dot");
			$(".selecteddot").attr("src","images/dot.png");
			$(".selecteddot").removeClass("selecteddot");
			$("#"+(introbgid+1)).removeClass("dot");
			$("#"+(introbgid+1)).toggleClass( "selecteddot" );
			$("#"+(introbgid+1)).attr("src","images/dotsel.png");
		}
	}
}

function changeintrobg(newbg)
{
	$(intro).css("background", "url('"+newbg+"') no-repeat center");
}

function changedot()
{
	$(".selecteddot").toggleClass("dot");
	newsrc="images/dot.png";
	$(".selecteddot").attr("src",newsrc);
	$(".selecteddot").removeClass("selecteddot");
	
	$(this).removeClass("dot");
	$(this).toggleClass( "selecteddot" );
	this.src = "images/dotsel.png";
	
	introbgid = this.id-1;
	animateintrobg(true);
}