//Burger Menu Toggle and Close Discount Message
const menuBtn = document.querySelector('.menu-btn')
const mobileNavMenu = document.getElementById('mobile-nav')


//Toggle Burger menu and mobile menu navigation
menuBtn.addEventListener('click', () => {
    menuBtn.classList.toggle('open')
    mobileNavMenu.classList.toggle('active')
})

//Accordion dropdowns for Mobile Navigation
const mobileNavLinks = document.querySelectorAll('.mobile-navbar-link')
const menuItems = document.querySelectorAll('.mobile-navbar-item')

menuItems.forEach(item => {
    item.addEventListener('click', e => {
        menuItems.forEach(otherItem => {
            if (otherItem !== item) {
                otherItem.classList.remove('active')
            }
        })
        const subMenu = item.querySelector('.mobile-subMenu-items')
        if (subMenu.contains(e.target)) {
            e.stopPropagation()
        } else {
            item.classList.toggle('active')
        }
    })
})

//Swiper Slider Progress Bar Type
const progressCircle = document.querySelector('.autoplay-progress svg')
const progressContent = document.querySelector('.autoplay-progress span')

//SwiperJS Configurations,SplitType animation with GSAP
var swiper = new Swiper('.mySwiper', {
    spaceBetween: 30,
    loop: true,
    speed: 1000,
    effect: 'fade',
    autoplay: {
        delay: 8000,
        disableOnInteraction: false
    },
    pagination: {
        el: '.swiper-pagination',
        clickable: true
    },
    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev'
    },
    on: {
        slideChange: function () {
            const texts =
                this.slides[this.activeIndex].querySelectorAll('.slider-heading')
            texts.forEach(text => {
                new SplitType(text)
                gsap.to(text.querySelectorAll('.char'), {
                    y: 0,
                    stagger: 0.02,
                    delay: 0.05,
                    duration: 0.1
                })
            })
        },
        autoplayTimeLeft(s, time, progress) {
            progressCircle.style.setProperty('--progress', 1 - progress)
            progressContent.textContent = `${Math.ceil(time / 1000)}s`
        }
    }
})

var swiper = new Swiper('.categoriesSwiper', {
    slidesPerView: 1,
    spaceBetween: 30,
    freeMode: true,
    loop: true,
    pagination: {
        el: '.swiper-pagination',
        clickable: true
    },
    breakpoints: {
        992: {
            slidesPerView: 6,
            spaceBetween: 20
        },
        768: {
            slidesPerView: 3,
            spaceBetween: 80
        }
    }
})

var swiper = new Swiper('.productCardSlider', {
    slidesPerView: 1,
    spaceBetween: 30,
    loop: true,
    pagination: {
        el: '.swiper-pagination',
        clickable: true
    },
    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev'
    }
})

var swiper = new Swiper(".elementorSwiper", {
    slidesPerView: 4,
    spaceBetween: 0,
    autoplay: {
        delay: 2500,
        disableOnInteraction: false,
    },
    centeredSlides: true,
    freeMode: true,
    loop: true,
    breakpoints: {
        100: {
            slidesPerView: 1,
        },
        400: {
            slidesPerView: 2,
        },
        600: {
            slidesPerView: 3,
        },
        1000: {
            slidesPerView: 4,
        },
    },

});
//Scroll Navbar Animation
let navbar = document.getElementById('nav')
let navbarHeight = navbar.offsetHeight
let scrollPos = window.scrollY

function updateNavbar() {
    scrollPos = window.scrollY

    if (scrollPos > 800) {
        navbar.classList.add('sticky')
    } else {
        navbar.classList.remove('sticky')
    }
}

window.addEventListener('scroll', function () {
    updateNavbar()
})

updateNavbar()

//Open Basket Menu
const basketBtn = document.querySelector('.basket-btn')
const basketMobileBtn = document.querySelector('.mobile-nav-basket')
var basketMenu = document.querySelector('.basket-sidebar')
const basketClose = document.querySelector('.button-close')

basketBtn.addEventListener('click', e => {
    e.preventDefault()
    basketMenu.classList.toggle('active')
})

basketMobileBtn.addEventListener('click', e => {
    e.preventDefault()
    basketMenu.classList.toggle('active')
})

//basketClose.addEventListener('click', e => {
//  e.preventDefault()
//  basketMenu.classList.remove('active')
//})


function CloseBasket() {

    basketMenu = document.querySelector('.basket-sidebar')
    basketMenu.classList.toggle('active')
}

//Product Set Video and Close Events
const videoLink = document.getElementById('video-link')
const videoContainer = document.getElementById('video-container')
const closeVideo = document.querySelector('.remove-video')
const videoFrame = document.getElementById('video-frame')


// search
function openSearch() {
    document.getElementById("searchOverlay").style.display = "block";
    document.body.classList.add("no-scroll"); // Prevent scrolling
}

function closeSearch() {
    document.getElementById("searchOverlay").style.display = "none";
    document.body.classList.remove("no-scroll"); // Allow scrolling again
}

function checkEnter(event) {
    if (event.key === "Enter") {
        performSearch();
    }
}

function performSearch() {
    var searchQuery = document.getElementById("searchInput").value;
    alert("Search performed for: " + searchQuery);
    closeSearch(); // Optionally close the search after performing it
}



