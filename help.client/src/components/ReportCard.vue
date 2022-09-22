<template>
   <div class="elevation-3 rounded">
<p>{{report.title}}</p>
<p>{{report.body}}</p>
<p>{{report.rating}}</p>
<p>{{report.creator.name}}</p>
<img :src="report.creator.picture" alt="">
   </div>
</template>
<script>
export default {
    props: {
        report: {
            type: Object,
            required:true
        }
    },
setup() {
  return {};
},
};
</script>
<style> 
</style>