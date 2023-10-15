$(document).ready(function($){
	$('.sort-cate .evo-filter').click(function(){
		$('.ant-cate-content .aside-filter').toggleClass('active');
		$(this).toggleClass('active');
		if ($(window).width() < 768) {
			$('.ant-cate-content .sort-cate-left h3, .ant-cate-content .sort-cate-left ul').removeClass('active');
		}
	});
	if ($(window).width() < 768) {
		$('.sort-cate-left h3').on('click', function(e){
			e.preventDefault();var $this = $(this);
			$this.parents('.sort-cate-left').find('ul').toggleClass('active');
			$(this).toggleClass('active');
			$('.ant-cate-content .aside-filter, .ant-cate-content .evo-filter').removeClass('active');
			return false;
		});
	};
	$('.aside-filter .aside-hidden-mobile .aside-item .aside-title').on('click', function(e){
		e.preventDefault();
		var $this = $(this);
		$this.parents('.aside-filter .aside-hidden-mobile .aside-item').find('.aside-content').stop().slideToggle();
		$(this).toggleClass('active');
		return false;
	});
});