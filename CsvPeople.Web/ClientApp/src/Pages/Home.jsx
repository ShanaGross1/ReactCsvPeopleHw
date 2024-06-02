import React, { useEffect, useState } from 'react';
import axios from 'axios';

const Home = () => {

    const [people, setPeople] = useState([]);

    useEffect(() => {
        const getPeople = async () => {
            const { data } = await axios.get('/api/people/getpeople');
            setPeople(data);
        }
        getPeople();
    }, []);

    const onDeleteClick = async () => {
        await axios.post('/api/people/deleteall');
        setPeople([])
    }

    return (<div style={{ marginTop: 80 }}>
        <button onClick={onDeleteClick} className="btn btn-danger btn-lg w-100">Delete All</button>
        <table className='table table-bordered table-hover mt-4'>
            <thead>
                <tr>
                    <th>Id</th>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Age</th>
                    <th>Address</th>
                    <th>Email</th>
                </tr>
            </thead>
            <tbody>
                {people.map(p =>
                    <tr>
                        <td>{p.id}</td>
                        <td>{p.firstName}</td>
                        <td>{p.lastName}</td>
                        <td>{p.age}</td>
                        <td>{p.address}</td>
                        <td>{p.email}</td>
                    </tr>
                )}
            </tbody>
        </table>
    </div>)
};

export default Home;