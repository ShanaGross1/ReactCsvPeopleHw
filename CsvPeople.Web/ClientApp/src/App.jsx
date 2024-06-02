import React from 'react';
import { Route, Routes } from 'react-router-dom';
import Layout from './components/Layout';
import Home from './Pages/Home';
import Upload from './Pages/Upload';
import Generate from './Pages/Generate';
const App = () => {
    return (
        <Layout>
            <Routes>
                <Route path='/' element={<Home />} />
                <Route path='/generate' element={<Generate />} />
                <Route path='/upload' element={<Upload />} />

            </Routes>
        </Layout>
    );
}

export default App;