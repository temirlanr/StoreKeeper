import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

export default function AddTransfer(){
    return ShowAddTransfer();
}

const ShowAddTransfer = () => {
    const storages = StorageDropDown();
    const items = ItemDropDown();
    const navigate = useNavigate();

    useEffect(() => {
        var txt = document.getElementById( 'droptxt' ),
            content = document.getElementById( 'content' ),
            list = document.querySelectorAll( '.content input[type="checkbox"]' ),
            quantity = document.querySelectorAll( '.quantity' );
    
        txt.addEventListener( 'click', function() {
            content.classList.toggle( 'show' )
        } )

        // Close the dropdown if the user clicks outside of it
        window.onclick = function( e ) {
            if ( !e.target.matches( '.list' ) ) {
                if ( content.classList.contains( 'show' ) ) content.classList.remove( 'show' )
            }
        }

        list.forEach( function( item, index ) {
            item.addEventListener( 'click', function() {
                quantity[ index ].type = ( item.checked ) ? 'number' : 'hidden';
                calc()
            } )
        } )

        quantity.forEach( function( item ) {
            item.addEventListener( 'input', calc )
        } )

        function calc() {
            for ( var i = 0, arr = []; i < list.length; i++ ) {
                if ( list[ i ].checked ) arr.push( quantity[ i ].value + ' x ' + list[ i ].value )
            }

            txt.value = arr.join( ', ' )
        }
    })

    return (
        <div>
            <h1>Add Transfer</h1>
            <form action=''>
                <label>Transfer date and time: 
                    <input type='datetime-local' id='transfer-day' name='transfer-day' />
                </label>
                <label>Sender storage: 
                    <input list='sender-storages' id='sender-storage' name='sender-storage' />
                </label>
                <datalist id='sender-storages'>
                    { storages.map(storage => {
                        return (
                            <option value={ storage.id }>{ storage.name }</option>
                        )
                    })}
                </datalist>
                <label>Recipient storage: 
                    <input list='recipient-storages' id='recipient-storage' name='recipient-storage' />
                </label>
                <datalist id='recipient-storages'>
                    { storages.map(storage => {
                        return (
                            <option value={ storage.id }>{ storage.name }</option>
                        )
                    })}
                </datalist>
                <div id='dropdown' className="dropdown">
                    <label>Transfer items</label>
                    <input type="text" id="droptxt" className="list" readOnly />
                        <div id="content" className="content">
                            {items.map(item => {
                                return (
                                    <div className='list'>
                                        <input type='checkbox' id={'item' + item.id} className='list' value={item.id} />
                                        <label htmlFor={'item' + item.id} className='list'>{ item.name }</label>
                                        <input type='hidden' className='list quantity' />
                                    </div>
                                )
                            })}
                        </div>
                </div>
                <input type='button' value='Create' id='submit' onClick={ () => {
                    AddTransferRequest();
                    return navigate('http://localhost:3000');
                } } />
            </form>
        </div>
    );
}

const StorageDropDown = () => {
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

    return storages;
}

const ItemDropDown = () => {
    const [items, setItems] = useState([]);

    useEffect( () => {
        async function fetchData() {
            try {
                const res = await axios.get('http://localhost:5000/api/Item');
                setItems(res.data);
            } catch(err) {
                console.log(err);
            }
        }
        fetchData();
    }, []);

    return items;
}

const AddTransferRequest = () => {
    require('react-dom');
    window.React2 = require('react');
    console.log(window.React1 === window.React2);

    useEffect(() => {
        var list = document.querySelectorAll( '.content input[type="checkbox"]' ),
            quantity = document.querySelectorAll( '.quantity' );

        const transferItems = [];

        for ( var i = 0; i < list.length; i++ ) {
            transferItems.push({
                'itemId': list[i].value,
                'quantity': quantity[i].value
            })
        }

        var body = {
            'dateTime': document.getElementById('transfer-day').value,
            'fromStorageId': document.getElementById('transfer-day').value,
            'toStorageId': document.getElementById('recipient-storage').value,
            'transferItems': transferItems
        };

        console.log(body);

        axios.post('http://localhost:5000/api/Transfer', body).then(function (response) {
            console.log(response);
        }).catch(function (error) {
            console.log(error);
        })
    }, [])
}