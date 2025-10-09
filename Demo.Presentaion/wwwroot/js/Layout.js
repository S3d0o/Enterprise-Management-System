$(document).ready(function () {
    function updateMenu() {
        const $menu = $('.menu');
        const $wee = $('.wee');
        const $menuItems = $('.menu-item');
        let $currentItem = $('.current-menu-item');
        const menuOffset = $menu.offset().left;

        $wee.css({ opacity: 1 });

        // Get current controller and action from URL
        const currentPath = window.location.pathname.toLowerCase();
        const pathParts = currentPath.split('/').filter(part => part);
        const currentController = pathParts[0] || 'home';
        const currentAction = pathParts[1] || 'index';

        // Set current-menu-item based on matching controller and action
        $menuItems.removeClass('current-menu-item');
        const $matchedItem = $menuItems.filter(function () {
            const $link = $(this).find('a');
            const controller = $link.attr('asp-controller')?.toLowerCase() || '';
            const action = $link.attr('asp-action')?.toLowerCase() || '';
            return (controller === currentController && (action === currentAction || (currentAction === 'index' && !action)));
        });

        if ($matchedItem.length) {
            $matchedItem.addClass('current-menu-item');
            $currentItem = $matchedItem;
        } else {
            // Fallback to Home
            $menuItems.find('a[asp-controller="Home"][asp-action="Index"]').parent().addClass('current-menu-item');
            $currentItem = $('.current-menu-item');
        }

        // Set initial wee position
        if ($currentItem.length) {
            const $currentLink = $currentItem.find('a');
            const initialLeft = $currentLink.offset().left - menuOffset;
            const initialWidth = $currentLink.outerWidth();
            $wee.css({ left: initialLeft + 'px', width: initialWidth + 'px' });
        } else {
            $wee.css({ left: '0px', width: '0px' });
        }

        // Hover events
        $menuItems.off('mouseenter mouseleave'); // Prevent duplicate bindings
        $menuItems.on('mouseenter', function () {
            const $link = $(this).find('a');
            const left = $link.offset().left - menuOffset;
            const width = $link.outerWidth();
            $wee.css({ left: left + 'px', width: width + 'px' });
        });

        $menuItems.on('mouseleave', function () {
            $currentItem = $('.current-menu-item');
            if ($currentItem.length) {
                const $currentLink = $currentItem.find('a');
                const resetLeft = $currentLink.offset().left - menuOffset;
                const resetWidth = $currentLink.outerWidth();
                $wee.css({ left: resetLeft + 'px', width: resetWidth + 'px' });
            } else {
                $wee.css({ left: '0px', width: '0px' });
            }
        });
    }

    // Initial call
    updateMenu();

    // Debounced resize handler
    let resizeTimeout;
    $(window).on('resize', function () {
        clearTimeout(resizeTimeout);
        resizeTimeout = setTimeout(updateMenu, 100);
    });

    // Re-run on route changes (for ASP.NET MVC SPA-like navigation)
    $(document).on('pjax:complete', updateMenu); // If using PJAX or similar
});