// JavaScript Document
 $("document").ready(function($){
    var nav = $('#Navar');

    $(window).scroll(function () {
        if ($(this).scrollTop() > 200) {
			$("#Navar").css("position", "fixed");
			$("#Navar").css("z-index", "99999999")
	 
            nav.addClass("f-nav");
        } else {
			$("#Navar").css("position", "absolute");
             nav.removeClass("f-nav");
        }
		  
    });
});