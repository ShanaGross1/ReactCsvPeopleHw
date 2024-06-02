import { useState } from 'react';
import axios from 'axios'

const Generate = () => {

    const [amount, setAmount] = useState(0);

    const onGenerateClick = async () => {
        window.location.href = `/api/people/generate?amount=${amount}`;

    }


    return (
        <div className="d-flex vh-100" style={{ marginTop: 70 }}>
            <div className="d-flex w-100 justify-content-center align-self-center">
                <div className="row">
                    <div className="col-md-10">
                        <input onChange={e => setAmount(e.target.value)} className='form-control' placeholder='Amount' />
                    </div>
                    <div className='col-md-2'>
                        <button className='btn btn-primary' onClick={onGenerateClick}>Generate</button>
                    </div>
                </div>
            </div>
        </div>

    )
}
export default Generate;