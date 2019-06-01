var CACHE_NAME = 'static-v3';
var CACHE_FILE = [
    '/Dashboard/Index',
    '/Dashboard',
    '/Conta/Index',
    '/Categoria/Index',
    '/Lancamento/Index',
    '/CPFCNPJ/Index',
    '/offline.html',
    '/404.html',
    '/',
    '/js/DashBoard/index.js',
    '/js/DashBoard/indexCalendario.js',
    '/js/Home/Index.js'
];

self.addEventListener('install', function (event) {
    event.waitUntil(
        caches.open(CACHE_NAME).then(function (cache) {
            return cache.addAll(CACHE_FILE);
        })
    )
});

self.addEventListener('activate', function activator(event) {
    event.waitUntil(
        caches.keys().then(function (keys) {
            return Promise.all(keys
                .filter(function (key) {
                    return key.indexOf(CACHE_NAME) !== 0;
                })
                .map(function (key) {
                    return caches.delete(key);
                })
            );
        })
    );
});

//self.addEventListener('fetch', function (event) {
//    event.respondWith(
//        caches.match(event.request).then(function (cachedResponse) {
//            return cachedResponse || fetch(event.request);
//        })
//    );
//});

function addLink(url, responseToCache) {
    caches.open(CACHE_NAME)
        .then(function (cache) {
            cache.put(url, responseToCache);
        });
}

self.addEventListener('fetch', function (event) {

    event.respondWith(
        // Try the cache
        caches.match(event.request).then(function (response) {
            if (response) {

                return response;
            }
            return fetch(event.request).then(function (response) {

                if (!response || response.status !== 200 || response.type !== 'basic') {
                    return response;
                }


                if (response.status === 404) {

                    return caches.match('/404.html');
                }

                if (response.url.toLowerCase().indexOf("conta") > 0 ) {

                    if (response.url.toLowerCase().indexOf("index") > 0) {                        
                        addLink("/Conta/Index", response.clone());
                    }

                    if (response.url.toLowerCase().indexOf("edit") < 0
                        && response.url.toLowerCase().indexOf("create") < 0) {
                        addLink("/Conta/Index", response.clone());
                    }
                }

                if (response.url.toLowerCase().indexOf("categoria") > 0) {

                    if (response.url.toLowerCase().indexOf("index") > 0) {
                        addLink("/Categoria/Index", response.clone());
                    }

                    if (response.url.toLowerCase().indexOf("edit") < 0
                        && response.url.toLowerCase().indexOf("create") < 0) {
                        addLink("/Categoria/Index", response.clone());
                    }
                }

                if (response.url.toLowerCase().indexOf("lancamento") > 0 && response.url.toLowerCase().indexOf(".js") < 0) {

                    if (response.url.toLowerCase().indexOf("index") > 0) {
                        addLink("/Lancamento/Index", response.clone());
                    }

                    if (response.url.toLowerCase().indexOf("edit") < 0
                        && response.url.toLowerCase().indexOf("create") < 0) {
                        addLink("/Lancamento/Index", response.clone());
                    }


                    console.log(response.url.toLowerCase());
                }

                if (response.url.toLowerCase().indexOf("cpfcnpj") > 0
                ) {

                    if (response.url.toLowerCase().indexOf("index") > 0) {
                        addLink("/CPFCNPJ/Index", response.clone());
                    }

                    if (response.url.toLowerCase().indexOf("edit") < 0
                        && response.url.toLowerCase().indexOf("create") < 0) {

                        addLink("/CPFCNPJ/Index", response.clone());
                    }
                }


                //if () {
                //    console.log("Gera CHACE " + response.url);
                //}

                //caches.open(CACHE_NAME)
                //    .then(function (cache) {
                //        cache.put(event.request, responseToCache);
                //    });

                return response;
            });
        }).catch(function () {
            // If both fail, show a generic fallback:
            return caches.match('/offline.html');
        })




    );
});