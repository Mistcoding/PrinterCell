(() => {
    const DB_NAME = "MagazzinoDb";
    const DB_VERSION = 2;
    const STORE = "articoli";
    const IDX_CODICE = "ix_codice";
    let dbInstance = null;

    function openDb() {
        return new Promise((resolve, reject) => {
            if (dbInstance) return resolve(dbInstance);

            const request = indexedDB.open(DB_NAME, DB_VERSION);

            request.onupgradeneeded = function (event) {
                let db = event.target.result;
                let store;
                if (!db.objectStoreNames.contains(STORE)) {
                    store = db.createObjectStore(STORE, { keyPath: "Id", autoIncrement: true });
                } else {
                    store = request.transaction.objectStore(STORE);
                }
                if (!store.indexNames.contains(IDX_CODICE)) {
                    store.createIndex(IDX_CODICE, "Codice", { unique: true });
                }
            };

            request.onsuccess = function () {
                dbInstance = request.result;
                resolve(dbInstance);
            };

            request.onerror = function () {
                reject("Errore apertura IndexedDB");
            };
        });
    }

    async function withStore(mode, fn) {
        const db = await openDb();
        return new Promise((resolve, reject) => {
            const tx = db.transaction(STORE, mode);
            const store = tx.objectStore(STORE);
            const result = fn(store);
            tx.oncomplete = () => resolve(result?._result ?? result);
            tx.onerror = () => reject(tx.error?.message ?? "Errore transazione");
        });
    }

    window.indexedDbManager = {
        init: async () => { await openDb(); return true; },

        getAll: async () => await withStore("readonly", (store) => {
            const r = store.getAll();
            r.onsuccess = () => { r._result = r.result; };
            return r;
        }),

        add: async (articolo) => await withStore("readwrite", (store) => {
            const r = store.add(articolo);
            r.onsuccess = () => { r._result = r.result; };
            return r;
        }),

        update: async (articolo) => await withStore("readwrite", (store) => store.put(articolo)),

        delete: async (id) => await withStore("readwrite", (store) => store.delete(id)),

        existsCodice: async (codice) => await withStore("readonly", (store) => {
            const idx = store.index(IDX_CODICE);
            const r = idx.get(codice);
            r.onsuccess = () => { r._result = !!r.result; };
            return r;
        })
    };
})();
