<%@ Page Language="C#" AutoEventWireup="true"  Inherits="Elenic.ComponentTypes" %>


<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">

    <title>jQuery Wookmark Plug-in Example</title>

    <!-- CSS Reset -->
    <link rel="stylesheet" href="../Content/reset.css">

    <!-- Global CSS for the page and tiles -->
    <link rel="stylesheet" href="../Content/main.css">
</head>

<body>
    <div class="progress-bar"></div>
    <div id="container">
        <header>
            <h1>jQuery Wookmark Plug-in Example</h1>
            <p>Resize the browser window or click a grid item to trigger layout updates.</p>
        </header>
        <div id="main" role="main">

            <ul runat="server" id="tiles"  OnClick="DoMyOnClickCall">
                <!-- These are our grid blocks -->
                <li>
                    <div class="nailthumb-container"><img src="../Images/CompTypeThumbs/DT Thumb.png"><p>Base Double</p></div>
                </li>
                <li>
                    <div class="nailthumb-container"><img src="../Images/CompTypeThumbs/LS Thumb.png"><p>Base Single</p></div>
                </li>
                <li>
                    <div class="nailthumb-container"><img src="../Images/CompTypeThumbs/LW Thumb.png"><p>Tall Double</p></div>
                </li>
                <li>
                    <div class="nailthumb-container"><img src="../Images/CompTypeThumbs/TS Thumb.png"><p>Tall Single</p></div>
                </li>
                <li>
                   <div class="nailthumb-container"> <img src="../Images/CompTypeThumbs/TW Thumb.png"><p>Wall Units Double</p></div>
                </li>
                <li>
                    <div class="nailthumb-container"><img src="../Images/CompTypeThumbs/WU Thumb.png"><p>Wall Units Single</p></div>
                </li>

                <!-- End of grid blocks -->
            </ul>

        </div>
    </div>

    <!-- include jQuery -->
    <script src="../Scripts/jquery.min.js"></script>

    <script type="text/javascript" src="../Scripts/nailthumb/jquery.nailthumb.1.1.min.js"></script>

    <!-- Include the imagesLoaded plug-in -->
    <script src="../Scripts/jquery.imagesloaded.js"></script>

    <!-- Include the plug-in -->
    <script src="../Scripts/jquery.wookmark.min.js"></script>

<%--    <script type="text/javascript">
        jQuery(document).ready(function () {
            jQuery('.nailthumb-container').nailthumb({ width: 135, height: 85 });
        });
     </script>--%>

    <!-- Once the page is loaded, initalize the plug-in. -->
    <script type="text/javascript">

        (function ($) {
            var loadedImages = 0, // Counter for loaded images
                handler = $('#tiles li'); // Get a reference to your grid items.
            // Prepare layout options.
            var options = {
                autoResize: true, // This will auto-update the layout when the browser window is resized.
                container: $('#main'), // Optional, used for some extra CSS styling
                offset: 5, // Optional, the distance between grid items
                outerOffset: 10, // Optional, the distance to the containers border
                itemWidth: 210 // Optional, the width of a grid item
            };

            $('#tiles').imagesLoaded(function () {
                // Call the layout function.
                handler.wookmark(options);

                // Capture clicks on grid items.
                handler.click(function () {
                    //alert($('img', this).attr('src'));
                    alert($('a', this).attr('href'));

                    // Randomize the height of the clicked item.
                    //var newHeight = $('img', this).height() + Math.round(Math.random() * 300 + 30);
                    //$(this).css('height', newHeight + 'px');

                    // Update the layout.
                    handler.wookmark();
                });
            }).progress(function (instance, image) {
                // Update progress bar after each image load
                loadedImages++;
                if (loadedImages == handler.length)
                    $('.progress-bar').hide();
                else
                    $('.progress-bar').width((loadedImages / handler.length * 100) + '%');
            });
        })(jQuery);
  </script>

</body>
</html>
