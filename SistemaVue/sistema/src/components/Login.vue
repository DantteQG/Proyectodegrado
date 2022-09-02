<template>
    <v-layout aling-center justify-center>
        <v-flex sx12 sm8 md 6 lg5 x14>
            <v-card>
                <div class="layout column align-center">
                  <img src="static/logo.png" alt="Vue Material Admin" width="180" height="180">
                  <h1 class="flex my-4 primary--text">Coorporacion PROESA</h1>
                </div>
                <v-toolbar dark color="blue dark-3">
                    <v-toolbar-title>
                        Acceso al sistema
                    </v-toolbar-title> 
                </v-toolbar>
                <v-card-text>
                    <v-text-field v-model="usuario" autofocus color="accent" label="Usuario" required  append-icon="person">
                    </v-text-field>
                    <v-text-field v-model="password" type="password" color="accent" label="Contrasena" required @keyup.enter="ingresar">
                    </v-text-field>
                    <v-flex class="red--text" v-if="error">
                        {{error}}
                    </v-flex>
                </v-card-text>
                <v-card-actions class="px-5 pb-5">
                    <v-spacer></v-spacer>
                    <v-btn color="primary" @click="ingresar">Ingresar</v-btn>
                </v-card-actions>
            </v-card>
        </v-flex>
    </v-layout>
    
</template>
<script>

import axios from 'axios'
export default {
    data(){
        return{
            usuario:'',
            password:'',
            error: null
        }
    },
    methods :{
        ingresar(){
            this.error=null;
            axios.post('api/Usuarios/Login',{usuario:this.usuario,password:this.password})
            .then(respuesta =>{
                return respuesta.data
            })
            .then(data => {
                this.$store.dispatch("guardarToken",data.token)
                this.$router.push({name: 'poraprobar'})
            })
            .catch(err => {
                //console.log(err.response)
                if(err.response.status == 400)
                {
                    this.error="No es un usuario válido"
                }
                else if(err.response.status == 404)
                {
                    this.error="No existe el usuario o sus datos son incorrectos"
                }
                else
                {
                    this.error="Ocurrió un error"
                }
                //console.log(err)
            })

        }
    }

    
}
</script>
