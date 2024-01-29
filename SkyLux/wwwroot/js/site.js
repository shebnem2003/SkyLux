
window.addEventListener("scroll", function () {
    if (window.scrollY >= 70) {
        document.querySelector("#layout1-nav").style.backgroundColor = "white"
        $("#layout1-nav").addClass("active")
        $("#layout1-nav a").addClass("active")
        $("#layout1-nav .setting").addClass("active2")
        $("#layout1-nav .setting").addClass("white")
        $("#layout-nav .setting").addClass("active2")
        $("#layout-nav .setting").addClass("white")
    }
    else {
        document.querySelector("#layout1-nav").style.backgroundColor = ""
        $("#layout1-nav").removeClass("active")
        $("#layout1-nav a").removeClass("active")
        $("nav .setting").css("display", "none")
        $("#layout1-nav .setting").removeClass("active2")
        $("#layout-nav .setting").removeClass("active2")
    }
})

$(".fa-bars").click(function () {
    $(".nav-responsive").fadeToggle()
    $(".setting").hide()
})
$(".lang").click(function () {
    $(".nav-responsive").hide()
})


$("header").click(function () {
    $(".setting").hide()
})

////$(".lang").click(function () {
////    $(".setting").fadeToggle()
////    $(".nav-responsive").hide()
////})



    
var deyer = 1
var total = $(".Flex p")
var total1=0
$(".div-passenger .fa-minus").click(function () {
    if (deyer <= 1) {
        deyer = 1
    }
    else {
        deyer--
    }
    $(".div-passenger p").text(deyer)
    total1 = Number(total.val()) * deyer

})

$(".div-passenger .fa-plus").click(function () {
    deyer++
    $(".div-passenger p").text(deyer)
    total1 = Number(total.val()) * deyer
})






//$(".container2 button").click(function () {
//    alert();
//    var inputValue = $("#inputval").val();
//    if (inputValue === "") {
//        $("#inputval").css("borderColor", "red")
//    }else{
//        $(".section").show()
//    }

//})



var swiper = new Swiper(".mySwiper", {
    spaceBetween: 0,
    centeredSlides: true,
    autoplay: {
        delay: 2750,
        disableOnInteraction: false
    },

});





window.addEventListener("scroll", function () {
    console.log(window.scrollY);
    if (window.scrollY >= 2000) {

        document.querySelector(".box1").style.animationName = "image"
        document.querySelector(".box2").style.animationName = "text"
    } else {
        document.querySelector(".box1").style.animationName = ""
        document.querySelector(".box2").style.animationName = ""
    }
})


$.get('https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/azerbaijan/today?unitGroup=metric&key=CR9C9C2CJYET5ET947TX2NUSU', (res) => {
    console.log(res)
    console.log(res.days[0].temp)
    $(".weather span").text(res.days[0].temp)
})
setInterval(() => {
    const date = new Date();
    console.log(date.getHours() + ' : ' + date.getMinutes())
    $(".hours").text(date.getHours() + ' : ' + date.getMinutes())
}, 1000)
