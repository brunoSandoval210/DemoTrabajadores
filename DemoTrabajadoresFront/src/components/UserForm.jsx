import React, { useState, useEffect } from 'react';
import axios from 'axios';
import Swal from 'sweetalert2';

export const UserForm = ({ userSelected, handlerAddUser, handlerCloseForm, initialUserForm }) => {
    const [departamentos, setDepartamentos] = useState([]);
    const [departamentoSeleccionado, setDepartamentoSeleccionado] = useState('');
    const [provinciaSeleccionada, setProvinciaSeleccionada] = useState('');
    const [distritoSeleccionado, setDistritoSeleccionado] = useState('');
    const [provincias, setProvincias] = useState([]);
    const [distritos, setDistritos] = useState([]);
    const [tipoDocumentoSeleccionado, setTipoDocumentoSeleccionado] = useState('');
    const [nroDocumentos, setNroDocumentos] = useState('');
    const [nombre, setNombre] = useState('');
    const [tipoSexoSeleccionado, setTipoSexoSeleccionado] = useState('');
    const [userForm, setUserForm] = useState(initialUserForm);
    const { idTrabajador, tipoDocumento, nroDocumento, nombres, sexo, idDistrito } = userForm;
    useEffect(() => {
        cargarDepartamentos();
    }, []);

    useEffect(() => {
        if (departamentoSeleccionado) {
            cargarProvincias();
        }
    }, [departamentoSeleccionado]);

    useEffect(() => {
        if (provinciaSeleccionada) {
            cargarDistritos();
        }
    }, [provinciaSeleccionada]);

    useEffect(() => {
        setUserForm({ ...userSelected });
        setTipoDocumentoSeleccionado(userSelected.tipoDocumento);
        setNroDocumentos(userSelected.nroDocumento);
        setNombre(userSelected.nombres);
        setTipoSexoSeleccionado(userSelected.sexo);
        setDepartamentoSeleccionado(userSelected.idDepartamento);
        setProvinciaSeleccionada(userSelected.idProvincia);
        setDistritoSeleccionado(userSelected.idDistrito);

    }, [userSelected]);

    const cargarDepartamentos = async () => {
        try {
            const resultado = await axios.get("https://localhost:7150/departamentos");
            setDepartamentos(resultado.data);
        } catch (error) {
            console.error('Error al cargar la lista de usuarios', error);
        }
    };

    const cargarProvincias = async () => {
        try {
            const resultado = await axios.get(`https://localhost:7150/provincias/departamento/${departamentoSeleccionado}`);
            setProvincias(resultado.data);
        } catch (error) {
            console.error('Error al cargar la lista de provincias', error);
        }
    };

    const cargarDistritos = async () => {
        try {
            const resultado = await axios.get(`https://localhost:7150/distritos/provincia/${provinciaSeleccionada}`);
            setDistritos(resultado.data);
        } catch (error) {
            console.error('Error al cargar la lista de distritos', error);
        }
    };

    const onDepartamentoChange = (e) => {
        const selectedDepartamento = e.target.value;
        setDepartamentoSeleccionado(selectedDepartamento);
        setProvinciaSeleccionada(''); // Reiniciar provincia
        setDistritoSeleccionado(''); // Reiniciar distrito
    };


    const onProvinciaChange = (e) => {
        setProvinciaSeleccionada(e.target.value);
    };
    const onDistritoChange = (e) => {
        setDistritoSeleccionado(e.target.value);
        setUserForm({ ...userForm, idDistrito: e.target.value });
    };

    const onTipoDocumentoChange = (e) => {
        setTipoDocumentoSeleccionado(e.target.value);
        setUserForm({ ...userForm, tipoDocumento: e.target.value });
        setNroDocumentos('');
    };

    const onNroDocumentoChange = (e) => {
        const newNDocumento = e.target.value.replace(/\D/g, '');
        setUserForm({ ...userForm, nroDocumento: newNDocumento });
        setNroDocumentos(newNDocumento);
    };

    const onNombresChanges = (e) => {
        const inputValue = e.target.value.replace(/[^a-zA-Z\s]/g, '');
        setUserForm({ ...userForm, nombres: inputValue });
        setNombre(inputValue);
    };

    const onTipoSexoChange = (e) => {
        setTipoSexoSeleccionado(e.target.value);
        setUserForm({ ...userForm, sexo: e.target.value });
    };

    const onCloseForm = () => {
        handlerCloseForm();
        setUserForm(initialUserForm);
        setTipoDocumentoSeleccionado('');
        setNroDocumentos('');
        setNombre('');
        setTipoSexoSeleccionado('');
        setDepartamentoSeleccionado('');
        setProvinciaSeleccionada('');
        setDistritoSeleccionado('');
    };
    const onSubmit = async (e) => {
        e.preventDefault();
        const requiredLength = tipoDocumentoSeleccionado === 'DNI' ? 8 : 12;
        if (nroDocumentos.length !== requiredLength) {
            Swal.fire(
                'Error de validación',
                `Debe ingresar ${requiredLength} dígitos para el ${tipoDocumentoSeleccionado}.`,
                'error'
            );
            return;
        }
        if (!tipoDocumento || !nroDocumento || !nombres || !sexo || !idDistrito) {
            Swal.fire(
                'Error de validación',
                'Debe completar los campos del formulario!',
                'error'
            );
            return;
        }   
        handlerAddUser(userForm);
    };
    
    return (
        <form onSubmit={onSubmit} >
            <select
                className='form-control my-3 w-75'
                value={tipoDocumentoSeleccionado}
                onChange={onTipoDocumentoChange}
            >
                <option value='' disabled>Seleccione el tipo de documento</option>
                <option value='DNI'>DNI</option>
                <option value='Pasaporte'>Pasaporte</option>
            </select>
            {tipoDocumentoSeleccionado === 'DNI' && (
                <input
                    className='form-control my-3 w-75'
                    placeholder='Ingrese los 8 dígitos del DNI'
                    name='nroDocumento'
                    value={nroDocumentos}
                    onChange={onNroDocumentoChange}
                    maxLength={8}
                />
            )}

            {tipoDocumentoSeleccionado === 'Pasaporte' && (
                <input
                    className='form-control my-3 w-75'
                    placeholder='Ingrese los 12 dígitos del pasaporte'
                    name='nroDocumento'
                    value={nroDocumentos}
                    onChange={onNroDocumentoChange}
                    maxLength={12}
                />
            )}
            <input
                className='form-control my-3 w-75'
                placeholder='Ingrese sus nombres y apellidos'
                value={nombre}
                name='nombres'
                onChange={onNombresChanges} />

            <select
                className='form-control my-3 w-75'
                value={tipoSexoSeleccionado}
                onChange={onTipoSexoChange}
                placeholder='Seleccione su tipo de sexo'
                name='sexo'
            >
                <option value='' disabled>Seleccione su tipo de sexo</option>
                <option value='M'>Masculino</option>
                <option value='F'>Femenino</option>
            </select>

            <select
                className='form-control my-3 w-75'
                value={departamentoSeleccionado}
                onChange={onDepartamentoChange}
            >
                <option value='' disabled>Seleccione su departamento</option>
                {departamentos.map(departamento => (
                    <option key={departamento.idDepartamento} value={departamento.idDepartamento}>
                        {departamento.nombreDepartamento}
                    </option>
                ))}
            </select>
            {departamentoSeleccionado && (
                <select
                    className='form-control my-3 w-75'
                    value={provinciaSeleccionada}
                    onChange={onProvinciaChange}
                >
                    <option value='' disabled>Seleccione su provincia</option>
                    {provincias.map(provincia => (
                        <option key={provincia.idProvincia} value={provincia.idProvincia}>
                            {provincia.nombreProvincia}
                        </option>
                    ))}
                </select>
            )}
            {provinciaSeleccionada && (
                <select
                    className='form-control my-3 w-75'
                    value={distritoSeleccionado}
                    onChange={onDistritoChange}
                >
                    <option value='' disabled>Seleccione su distrito</option>
                    {distritos.map(distrito => (
                        <option key={distrito.idDistrito} value={distrito.idDistrito}>
                            {distrito.nombreDistrito}
                        </option>
                    ))}
                </select>
            )}
            <input type="hidden"
                name='id'
                value={idTrabajador} />
            <button
                type='submit'
                className='btn btn-primary'>
                {idTrabajador > 0 ? 'Actualizar' : 'Guardar'}
            </button>
            <button
                className='btn btn-primary mx-2'
                type='button'
                onClick={()=> onCloseForm()}
            >
                Cerrar
            </button>
        </form>
    );
};

