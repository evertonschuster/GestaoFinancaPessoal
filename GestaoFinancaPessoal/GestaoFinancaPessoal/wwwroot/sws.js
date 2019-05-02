

var CACHE_NAME = 'usersCacheV1'; // {1}

var CACHE_FILES = [ // {2}
    'conta',
    'home',
    '~/assets/css/lib/weather-icons.css',
    '~/assets/css/lib/owl.carousel.min.css',
    '~/assets/css/lib/owl.theme.default.min.css',
    '~/assets/css/lib/font-awesome.min.css',
    '~/assets/css/lib/themify-icons.css',
    '~/assets/css/lib/menubar/sidebar.css',
    '~/assets/css/lib/bootstrap.min.css',

];

self.addEventListener('install', (event) => {
    event.waitUntil(
        caches.open(CACHE_NAME) // {3}
            .then((cache) => { // {4}
                return cache.addAll(CACHE_FILES);
            })
            .then(() => self.skipWaiting()) // {5}
    )
});

self.addEventListener('activate', function (event) {
    var cacheWhitelist = ['usersCacheV2', 'my-other-cache'];
    event.waitUntil(
        caches.keys().then(function (cacheNames) {
            return Promise.all(
                cacheNames.map(function (cacheName) {
                    if (cacheWhitelist.indexOf(cacheName) === -1) {
                        return caches.delete(cacheName);
                    }
                })
            );
        })
    );
});

self.addEventListener('fetch', function (event) {
    event.respondWith(
        fetch(event.request).catch(function (error) { // {9}
            return caches.open(CACHE_NAME).then(function (cache) {
                return cache.match('offline.html'); // {10}
            });
        }));
});

function cacheFirstStrategy(request) {
    return caches.match(request) // {11}
        .then(function (cacheResponse) {
            return cacheResponse // {12}
                || fetchRequestAndCache(request); // {13}
        });
}

function fetchRequestAndCache(request) {
    return fetch(request) // {14}
        .then(function (networkResponse) {
            caches.open(getCacheName(request)) // {15}
                .then(function (cache) {
                    cache.put(request, networkResponse); // {16}
                });
            return networkResponse.clone(); // {17}
        });
}

function networkFirstStrategy(request) {
    return fetchRequestAndCache(request) // {18}
        .catch(function (response) { // {19}
            return caches.match(request); // {20}
        });
}
