$(document).ready(function () {
    // Cool nav menu with sliding underline
    function updateMenu() {
        var $menu = $('.menu');
        var $wee = $('.wee');
        var $menuItems = $('.menu-item');
        var $currentItem = $('.current-menu-item');
        var menuOffset = $menu.offset().left;

        // Ensure wee is visible
        $wee.css({ opacity: 1 });

        console.log('Menu offset:', menuOffset); // Debug log

        // Get current controller and action from URL
        var currentPath = window.location.pathname.toLowerCase();
        console.log('Current path:', currentPath); // Debug log
        var pathParts = currentPath.split('/').filter(function (part) { return part; });
        var currentController = pathParts[0] || 'home'; // Default to 'home' if empty
        var currentAction = pathParts[1] || 'index'; // Default to 'index' if empty
        if (!currentAction && currentPath !== '/') {
            currentAction = 'index'; // Assume 'index' if only controller is present (e.g., /Departments)
        }
        console.log('Current route:', { controller: currentController, action: currentAction }); // Debug log

        // Set current-menu-item based on matching controller and action
        $menuItems.removeClass('current-menu-item');
        var $matchedItem = $menuItems.filter(function () {
            var $link = $(this).find('a');
            var controller = $link.attr('asp-controller') ? $link.attr('asp-controller').toLowerCase() : '';
            var action = $link.attr('asp-action') ? $link.attr('asp-action').toLowerCase() : '';
            return (controller === currentController && (action === currentAction || (currentAction === 'index' && !action)));
        });

        if ($matchedItem.length) {
            $matchedItem.addClass('current-menu-item');
            $currentItem = $matchedItem;
            console.log('Set current-menu-item:', $currentItem.find('a').text()); // Debug log
        } else {
            // Fallback to Home if no match
            $menuItems.find('a[asp-controller="Home"][asp-action="Index"]').parent().addClass('current-menu-item');
            $currentItem = $('.current-menu-item');
            console.log('No matching route, defaulting to Home'); // Debug log
        }

        // Set initial position under current-menu-item
        if ($currentItem.length) {
            var $currentLink = $currentItem.find('a');
            var initialLeft = $currentLink.offset().left - menuOffset;
            var initialWidth = $currentLink.outerWidth();
            console.log('Initial wee position:', { left: initialLeft, width: initialWidth }); // Debug log
            $wee.css({ left: initialLeft + 'px', width: initialWidth + 'px' });
        } else {
            console.log('No current-menu-item, setting wee to zero'); // Debug log
            $wee.css({ left: '0px', width: '0px' });
        }

        $menuItems.on('mouseenter', function () {
            var $link = $(this).find('a');
            var left = $link.offset().left - menuOffset;
            var width = $link.outerWidth();
            console.log('Hovering:', { item: $link.text(), left: left, width: width }); // Debug log
            $wee.css({ left: left + 'px', width: width + 'px' });
        });

        $menuItems.on('mouseleave', function () {
            // Reset to current-menu-item
            $currentItem = $('.current-menu-item');
            if ($currentItem.length) {
                var $currentLink = $currentItem.find('a');
                var resetLeft = $currentLink.offset().left - menuOffset;
                var resetWidth = $currentLink.outerWidth();
                console.log('Resetting wee to:', { left: resetLeft, width: resetWidth }); // Debug log
                $wee.css({ left: resetLeft + 'px', width: resetWidth + 'px' });
            } else {
                console.log('No current-menu-item, hiding wee'); // Debug log
                $wee.css({ left: '0px', width: '0px' });
            }
        });
    }

    // Initial call
    updateMenu();

    // Debounced resize handler
    let resizeTimeout;
    $(window).on('resize', function () {
        console.log('Window resized, updating menu'); // Debug log
        clearTimeout(resizeTimeout);
        resizeTimeout = setTimeout(updateMenu, 100);
    });
});