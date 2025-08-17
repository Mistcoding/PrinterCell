window.indexedDbManager = {
    init: function () {
        return new Promise((resolve, reject) => {
            const request = indexedDB.open("MagazzinoDb", 1);

            request.onupgradeneeded = function (event) {
                let db = event.target.result;
                if (!db.objectStoreNames.contains("articoli")) {
                    db.createObjectStore("articoli", { keyPath: "Id", autoIncrement: true });
                }
            };

            request.onsuccess = function () {
                resolve(true);
            };

            request.onerror = function () {
                reject("Errore IndexedDB init");
            };
        });
    },

    getAll: function () {
        return new Promise((resolve, reject) => {
            const request = indexedDB.open("MagazzinoDb", 1);

            request.onsuccess = function (event) {
                const db = event.target.result;
                const tx = db.transaction("articoli", "readonly");
                const store = tx.objectStore("articoli");
                const getReq = store.getAll();

                getReq.onsuccess = function () {
                    resolve(getReq.result);
                };
                getReq.onerror = function () {
                    reject("Errore getAll");
                };
            };
        });
    },

    add: function (articolo) {
        return new Promise((resolve, reject) => {
            const request = indexedDB.open("MagazzinoDb", 1);

            request.onsuccess = function (event) {
                const db = event.target.result;
                const tx = db.transaction("articoli", "readwrite");
                const store = tx.objectStore("articoli");
                const addReq = store.add(articolo);

                addReq.onsuccess = function () {
                    resolve(addReq.result);
                };
                addReq.onerror = function () {
                    reject("Errore add");
                };
            };
        });
    },

    update: function (articolo) {
        return new Promise((resolve, reject) => {
            const request = indexedDB.open("MagazzinoDb", 1);

            request.onsuccess = function (event) {
                const db = event.target.result;
                const tx = db.transaction("articoli", "readwrite");
                const store = tx.objectStore("articoli");
                const putReq = store.put(articolo);

                putReq.onsuccess = function () {
                    resolve(true);
                };
                putReq.onerror = function () {
                    reject("Errore update");
                };
            };
        });
    },

    delete: function (id) {
        return new Promise((resolve, reject) => {
            const request = indexedDB.open("MagazzinoDb", 1);

            request.onsuccess = function (event) {
                const db = event.target.result;
                const tx = db.transaction("articoli", "readwrite");
                const store = tx.objectStore("articoli");
                const delReq = store.delete(id);

                delReq.onsuccess = function () {
                    resolve(true);
                };
                delReq.onerror = function () {
                    reject("Errore delete");
                };
            };
        });
    }
};
