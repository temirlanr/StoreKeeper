import React, { useEffect, useState } from 'react';
import axios from 'axios';

export default function HomePage(){
    return ShowTransfers();
}

const ShowTransfers = () => {
    const [transfers, setTransfers] = useState([]);

    useEffect( () => {
        async function fetchData() {
            try {
                const res = await axios.get('http://localhost:5000/api/Transfer');
                setTransfers(res.data);
            } catch(err) {
                console.log(err);
            }
        }
        fetchData();
    }, []);

    console.log(transfers);

    return (
    <div className='transfer-container'>
        <h1>Transfers</h1>
        <a href='/storages'>
            <button>See Storages</button>
        </a>
        <a href="/add-transfer">
            <button>Add Transfer</button>
        </a>
        <table id='transfers' className='ws-table-all'>
            <thead>
                <tr>
                    <th>Date and time of transfer</th>
                    <th>Sender storage name</th>
                    <th>Recipient storage name</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                { transfers.map(transfer => {
                    return (
                        <tr>
                            <td>{ transfer.dateTime }</td>
                            <td>{ transfer.fromStorage.name }</td>
                            <td>{ transfer.toStorage.name }</td>
                            <td>
                                <button onClick={ () => axios.delete(
                                        'http://localhost:5000/api/Transfer/' + transfer.id
                                        ).then(function (response) {
                                            console.log(response);
                                        }).catch(function (error) {
                                            console.log(error);
                                        }) }>
                                    Delete
                                </button>
                            </td>
                        </tr>
                    )
                }) }
            </tbody>
        </table>
    </div>)
}