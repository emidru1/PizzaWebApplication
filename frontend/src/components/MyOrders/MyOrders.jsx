import React from 'react';
import useFetch from '../App/useFetch.js';
import { Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from '@mui/material';
import './MyOrders.css'; 

export default function MyOrders() {
    const {data: orders, error} = useFetch('http://localhost:5290/api/Order');

    if(!orders) return <div>Loading...</div>;
    if(error) return <div>Error: {error}</div>;

    return (
        <div className="my-orders">
            <TableContainer component={Paper} elevation={3} style={{ maxWidth: '90%', margin: '0 auto', marginBottom: '2rem', borderRadius: '15px'}}>
                <Typography variant="h4" align="center">
                    My Order List
                </Typography>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>Order ID</TableCell>
                            <TableCell>Pizza size</TableCell>
                            <TableCell>Toppings</TableCell>
                            <TableCell>Total</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {
                            orders.map(order => (
                                <TableRow key={order.id}>
                                    <TableCell>{order.id}</TableCell>
                                    <TableCell>{order.size.name}</TableCell>
                                    <TableCell>{
                                        order.toppings 
                                        && 
                                        order.toppings.map(ot => ot.name).join(', ')
                                    }
                                    </TableCell>
                                    <TableCell>${order.orderTotal}</TableCell>
                                </TableRow>
                            ))
                        }
                    </TableBody>
                </Table>
            </TableContainer>
        </div>
    );
}
