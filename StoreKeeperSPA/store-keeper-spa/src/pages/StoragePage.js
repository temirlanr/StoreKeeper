import React, { useState, useEffect } from "react";
import axios from "axios";

export default function StoragePage() {
    return (
        ShowStorages()
    );
}

const ShowStorages = () => {
    const [storages, setStorages] = useState([]);

    useEffect( () => {
        async function fetchData() {
            try {
                const res = await axios.get('http://localhost:5000/api/Storage');
                setStorages(res.data);
            } catch(err) {
                console.log(err);
            }
        }
        fetchData();
    }, []);

    console.log(storages);

    return (
        <div className='storage-container'>
            <h1>Storages</h1>
            <table id='storages' className='ws-table-all'>
                <thead>
                    <tr>
                        <th>Storage name</th>
                        <th>Items and their quantity</th>
                    </tr>
                </thead>
                <tbody>
                    { storages.map( storage => {
                        return (
                            <tr>
                                <td>{ storage.name }</td>
                                <td>
                                    { storage.storageItems.map(storageItem => {
                                        return (
                                            <ul>
                                                <li>{ storageItem.item.name }: { storageItem.quantity }</li>
                                            </ul>
                                        )
                                    })}
                                </td>
                            </tr>
                        )
                    }) }
                </tbody>
            </table>
        </div>
    )
}