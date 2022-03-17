<template>
    <div>
        <v-container fluid>
            <h2>Students List</h2>
            <router-link :to="{name: 'Register'}"><v-btn color="secondary" small>Create Student</v-btn></router-link>
            <v-simple-table>
                <template v-slot:default>
                    <thead>
                        <tr >
                            <th class="text-left">RA(Registro do Aluno)</th>
                            <th class="text-left">Name</th>
                            <th class="text-left">Email</th>
                            <th class="text-left">CPF</th>
                            <th class="text-left">Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="student in students" :key="student.ra">
                            <td>{{student.ra}}</td>
                            <td>{{student.name}}</td>
                            <td>{{student.email }}</td>
                            <td>{{student.cpf}}</td>
                            <td>
                                <router-link :to="{name: 'Edit', params: {id: student.ra}}"><v-btn color="warning" small>Edit</v-btn></router-link> |
                                <v-dialog v-model="dialog" max-width="290" :retain-focus="false">
                                    <template v-slot:activator="{ on, attrs }">
                                        <v-btn color="error" small v-bind="attrs" @click='saveRA(student.ra)' v-on="on">Delete</v-btn>
                                    </template>
                                <v-card>
                                    <v-card-title class="text-h5">
                                    Delete Confirmation
                                    </v-card-title>
                                    <v-card-text>Are you sure that you want to delete this Student?</v-card-text>
                                    <v-card-actions>
                                    <v-spacer></v-spacer>
                                    <v-btn color="secondary" text @click="dialog = false">
                                        Cancel
                                    </v-btn>
                                    <v-btn color="secondary" text @click="deleteUser()">
                                        Yes, Delete it
                                    </v-btn>
                                    </v-card-actions>
                                </v-card>
                                </v-dialog>
                            </td>
                        </tr>
                    </tbody>
                </template>
            </v-simple-table>
        </v-container>
    </div>
</template>

<script>
import axios from 'axios';
export default {
    name: 'UserViews',
    created() {
        axios.get('https://localhost:7082/Students').then(res => {
            this.students = res.data;
        }).catch(err => {
            console.log(err);
        })
    },
    data() 
    {
        return{
            search: '',
            dialog: false,
            headers: [
                { text: 'RA',value: 'ra' },
                { text: 'Name', value: 'name' },
                { text: 'Email', value: 'email' },
                { text: 'CPF', value: 'cpf' },
                { text: 'actions', value: 'actions' }
            ],
            students:[],
            deleteUserId: -1
        }
    },
    methods: {
        deleteUser() {
            axios.delete('https://localhost:7082/Students/'+this.deleteUserId).then(res => {
                console.log(res);
                this.students = this.students.filter(s => s.ra != this.deleteUserId);
                this.dialog = false;
            }).catch(err => {
                console.log(err);
            })
        },
        saveRA(ra) {
            this.deleteUserId = ra;
        }
    }
}
</script>

<style scoped>

</style>