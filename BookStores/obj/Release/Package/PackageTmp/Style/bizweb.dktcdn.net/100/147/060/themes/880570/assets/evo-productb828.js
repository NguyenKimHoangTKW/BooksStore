var galleryThumbs = new Swiper('.gallery-thumbs', {
		spaceBetween: 5,
		slidesPerView: 10,
		lazy: false,
		hashNavigation: true,
		watchSlidesVisibility: true,
		watchSlidesProgress: true,
		direction: 'vertical',
		loop: false, 
		loopAdditionalSlides: 0,
		breakpoints: {
			300: {
				slidesPerView: 4,
				spaceBetween: 10,
				direction: 'horizontal',
			},
			500: {
				slidesPerView: 4,
				spaceBetween: 10,
				direction: 'horizontal',
			},
			640: {
				slidesPerView: 5,
				spaceBetween: 10,
				direction: 'horizontal',
			},
			768: {
				slidesPerView: 4,
				spaceBetween: 10,
				direction: 'horizontal',
			},
			1024: {
				slidesPerView: 5,
				spaceBetween: 10,
			},
		},
		navigation: {
			nextEl: '.swiper-button-next',
			prevEl: '.swiper-button-prev',
		},
	});
	var galleryTop = new Swiper('.gallery-top', {
		spaceBetween: 0,
		lazy: false,
		hashNavigation: true,
		thumbs: {
			swiper: galleryThumbs
		}
	});
	$(document).ready(function() {
		$("#lightgallery").lightGallery({
			thumbnail: false
		}); 
	});
var swiper = new Swiper('.ant-similar-product-swiper', {
	slidesPerView: 4,
	spaceBetween: 15,
	slidesPerGroup: 2,
	navigation: {
		nextEl: '.swiper-button-next',
		prevEl: '.swiper-button-prev',
	},
	breakpoints: {
		300: {
			slidesPerView: 2,
			spaceBetween: 7,
		},
		500: {
			slidesPerView: 2,
			spaceBetween: 10,
		},
		640: {
			slidesPerView: 2,
			spaceBetween: 10,
		},
		768: {
			slidesPerView: 3,
			spaceBetween: 10,
		},
		1024: {
			slidesPerView: 4,
			spaceBetween: 15,
		},
		1200: {
			slidesPerView: 5,
			spaceBetween: 15,
		}
	}
});
var mySwiper;
if ($(window).width() < 992){
	mySwiper = new Swiper('.ant-product-slide-image', {
		slidesPerView: 1,
		spaceBetween: 0,
		slidesPerGroup: 1,
		autoHeight: true,
		scrollbar: {
			el: ".swiper-scrollbar",
			hide: true,
		},
		breakpoints: {
			300: {
				slidesPerView: 1,
				spaceBetween: 0
			},
			500: {
				slidesPerView: 1,
				spaceBetween: 0
			},
			640: {
				slidesPerView: 1,
				spaceBetween: 0
			},
			768: {
				slidesPerView: 2,
				spaceBetween: 0
			}
		}
	});
};
jQuery(document).ready(function($){
	orientationChange();
	jQuery(document).ready(function(e) {
		var WindowHeight = jQuery(window).height();
		var load_element = 0;
		var scroll_position = jQuery('.details-pro').offset().top + jQuery('.details-pro').outerHeight(true);;
		var screen_height = jQuery(window).height();
		var activation_offset = 0;
		var max_scroll_height = jQuery('body').height() + screen_height;
		var scroll_activation_point = scroll_position - (screen_height * activation_offset);
		jQuery(window).on('scroll', function(e) {
			var y_scroll_pos = window.pageYOffset;
			var element_in_view = y_scroll_pos > scroll_activation_point;
			var has_reached_bottom_of_page = max_scroll_height <= y_scroll_pos && !element_in_view;
			if (element_in_view || has_reached_bottom_of_page) {
				jQuery('.ant-form-product').addClass("ins-Drop");
			} else {
				jQuery('.ant-form-product').removeClass("ins-Drop");
			}
		});
	});
});

function orientationChange() {
	if(window.addEventListener) {
		window.addEventListener("orientationchange", function() {
			location.reload();
		});
	}
}
$(document).on('click', '.js-copy',function(e){
	e.preventDefault();
	var copyText = $(this).attr('data-copy');
	var copyTextarea = document.createElement("textarea");
	copyTextarea.textContent = copyText;
	copyTextarea.style.position = "fixed";
	document.body.appendChild(copyTextarea);
	copyTextarea.select();
	document.execCommand("copy"); 
	document.body.removeChild(copyTextarea);
	var cur_text = $(this).text();
	var $cur_btn = $(this);
	$(this).addClass("iscopied");
	$(this).text("Đã lưu");
	setTimeout(function(){
		$cur_btn.removeClass("iscopied");
		$cur_btn.text(cur_text);
	},1500)
})